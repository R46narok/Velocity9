using System.Text.RegularExpressions;
using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Commands;

public record CreateMuscleCommand(
    string Name, string Description,
    float TypeOneFiberPercentage, float TypeTwoAFiberPercentage, float TypeTwoXFiberPercentage,
    string Group
    ) : IRequest<ApiResponse>;

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, ApiResponse>
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
    
    public async Task<ApiResponse> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
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