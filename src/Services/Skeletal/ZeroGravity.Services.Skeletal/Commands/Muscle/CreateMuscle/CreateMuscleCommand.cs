using System.Text.RegularExpressions;
using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public record CreateMuscleCommand(
    string Name, string Description,
    float TypeOneFiberPercentage, float TypeTwoAFiberPercentage, float TypeTwoXFiberPercentage,
    string Group
    ) : IRequest<PipelineResult>;

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IMuscleRepository _muscleRepository;
    private readonly IMuscleGroupRepository _muscleGroupRepository;

    public CreateMuscleCommandHandler(IMapper mapper, IMuscleRepository muscleRepository, IMuscleGroupRepository muscleGroupRepository)
    {
        _mapper = mapper;
        _muscleRepository = muscleRepository;
        _muscleGroupRepository = muscleGroupRepository;
    }
    
    public async Task<PipelineResult> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
    {
        var group = await _muscleGroupRepository.GetByNameAsync(request.Group);
        var entity = _mapper.Map<Muscle>(request);
        entity.Group = group;
        await _muscleRepository.CreateAsync(entity);

        group.Muscles.Add(entity);
        await _muscleGroupRepository.UpdateAsync(group);

        return new("Created");
    }
}