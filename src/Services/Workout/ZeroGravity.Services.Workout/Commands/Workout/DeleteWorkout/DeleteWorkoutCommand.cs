using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record DeleteWorkoutCommand(string WorkoutName, string UserName) : IRequest<CqrsResult>;

public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, CqrsResult>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMapper _mapper;
    
    public DeleteWorkoutCommandHandler(IWorkoutRepository repository, IMessagePublisher messagePublisher, IMapper mapper)
    {
        _repository = repository;
        _messagePublisher = messagePublisher;
        _mapper = mapper;
    }

    public async Task<CqrsResult> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.UserName, request.WorkoutName);
        await _repository.DeleteAsync(entity!);

        var @event = _mapper.Map<WorkoutDeletedEvent>(entity);
        _messagePublisher.PublishTopicAsync(@event, MessageMetadata.Now());
        
        return new();
    }
}
