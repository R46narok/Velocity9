using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record CreateMuscleCommandResponse(int Id);

public class CreateMuscleCommand : IRequest<ErrorOr<CreateMuscleCommandResponse>>
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
}

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, ErrorOr<CreateMuscleCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleRepository _repository;

    public CreateMuscleCommandHandler(IMapper mapper, IMuscleRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ErrorOr<CreateMuscleCommandResponse>> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Muscle>(request);
        var id = await _repository.CreateAsync(entity);

        return new CreateMuscleCommandResponse(id);
    }
}