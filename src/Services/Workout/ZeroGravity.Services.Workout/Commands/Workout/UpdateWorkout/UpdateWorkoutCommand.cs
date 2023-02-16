using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record UpdateWorkoutCommand(
        string UserName, string WorkoutName, string? NewWorkoutName, string? Notes, DateTime? CompletedOn) 
 : IRequest<PipelineResult>;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand, PipelineResult>
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

    public async Task<PipelineResult> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _repository.GetByNameAsync(request.UserName, request.WorkoutName))!;

        entity.Name = request.NewWorkoutName ?? entity.Name;
        entity.Notes = request.Notes ?? entity.Notes;
        entity.CompletedOn = request.CompletedOn ?? entity.CompletedOn;

        var @event = _mapper.Map<WorkoutUpdatedEvent>(entity);
        
        await _repository.UpdateAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new();
    }
}
 