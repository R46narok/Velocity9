using System.IO.Pipelines;
using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record CreateSetCommandResponse(int Id);

public record CreateSetCommand
    (string Notes, int TargetReps, int CompletedReps,
        string ExerciseName, string WorkoutName, string UserName) 
    : IRequest<ErrorOr<CreateSetCommandResponse>>;

public class CreateSetCommandHandler : IRequestHandler<CreateSetCommand, ErrorOr<CreateSetCommandResponse>>
{
    private readonly ISetRepository _setRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;

    public CreateSetCommandHandler(ISetRepository setRepository, IExerciseRepository exerciseRepository, IMessagePublisher publisher, IMapper mapper, IWorkoutRepository workoutRepository)
    {
        _setRepository = setRepository;
        _exerciseRepository = exerciseRepository;
        _publisher = publisher;
        _mapper = mapper;
        _workoutRepository = workoutRepository;
    }

    public async Task<ErrorOr<CreateSetCommandResponse>> Handle(CreateSetCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Set>(request);

        entity.Exercise = (await _exerciseRepository.GetByNameAsync(request.ExerciseName))!;
        entity.Workout = (await _workoutRepository.GetByNameAsync(request.UserName, request.WorkoutName))!;

        var @event = _mapper.Map<SetCreatedEvent>(entity);

        var id = await _setRepository.CreateAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

       return new CreateSetCommandResponse(id);
    }
}