using AutoMapper;
using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public record CreateMuscleGroupCommand(string Name, string Description) : IRequest<PipelineResult>;

public class CreateMuscleGroupCommandHandler : IRequestHandler<CreateMuscleGroupCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IMuscleGroupRepository _repository;

    public CreateMuscleGroupCommandHandler(IMapper mapper, IMuscleGroupRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<PipelineResult> Handle(CreateMuscleGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MuscleGroup>(request);
        await _repository.CreateAsync(entity);

        return new(DetailsMessage.For(StatusCode.Created, nameof(MuscleGroup)));
    }
}