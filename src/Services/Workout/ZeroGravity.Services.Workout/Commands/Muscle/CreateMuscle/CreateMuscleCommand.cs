using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateMuscleCommand : IRequest<PipelineResult>
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
}

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IMuscleRepository _repository;

    public CreateMuscleCommandHandler(IMapper mapper, IMuscleRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PipelineResult> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Muscle>(request);
        await _repository.CreateAsync(entity);

        return new("Created");
    }
}