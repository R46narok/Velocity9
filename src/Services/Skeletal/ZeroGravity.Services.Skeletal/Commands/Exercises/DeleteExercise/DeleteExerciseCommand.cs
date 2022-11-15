using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.DeleteExercise;

public record DeleteExerciseCommand(string Name) : IRequest<ApiResponse>;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ApiResponse>
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
    
    public async Task<ApiResponse> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _repository.GetByNameAsync(request.Name))!;
        var id = entity.Id;
        await _repository.DeleteAsync(entity);
        
        var @event = new ExerciseDeletedEvent(id, request.Name);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new();
    }
}