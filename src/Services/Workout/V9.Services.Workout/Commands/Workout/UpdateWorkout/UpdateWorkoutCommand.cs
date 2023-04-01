using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public record UpdateWorkoutCommandResponse;
public record UpdateWorkoutCommand(
        string UserName, string WorkoutName, string? NewWorkoutName, string? Notes, DateTime? CompletedOn)
    : IRequest<ErrorOr<UpdateWorkoutCommandResponse>>;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand, ErrorOr<UpdateWorkoutCommandResponse>>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;

    public UpdateWorkoutCommandHandler(IWorkoutRepository repository, IMessagePublisher publisher, IMapper mapper)
    {
        _repository = repository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UpdateWorkoutCommandResponse>> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _repository.GetByNameAsync(request.UserName, request.WorkoutName))!;

        entity.Name = request.NewWorkoutName ?? entity.Name;
        entity.Notes = request.Notes ?? entity.Notes;
        entity.CompletedOn = request.CompletedOn ?? entity.CompletedOn;

        var @event = _mapper.Map<WorkoutUpdatedEvent>(entity);

        await _repository.UpdateAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new UpdateWorkoutCommandResponse();
    }
}