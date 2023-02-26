using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.DeleteExercise;

public record DeleteExerciseCommandResponse(string Name);
public record DeleteExerciseCommand(string Name) : IRequest<ErrorOr<DeleteExerciseCommandResponse>>;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ErrorOr<DeleteExerciseCommandResponse>>
{
    private readonly IExerciseRepository _repository;
    private readonly IMessagePublisher _publisher;

    public DeleteExerciseCommandHandler(
        IExerciseRepository repository,
        IMessagePublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }
    
    public async Task<ErrorOr<DeleteExerciseCommandResponse>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _repository.GetByNameAsync(request.Name))!;
        var id = entity.Id;
        await _repository.DeleteAsync(entity);
        
        var @event = new ExerciseDeletedEvent(id, request.Name);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new DeleteExerciseCommandResponse(request.Name);
    }
}