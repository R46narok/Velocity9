using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Exercises.DeleteExercise;

public class DeleteExerciseCommand : IRequest<ApiResponse>
{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public DeleteExerciseCommand(int? id = null, string? name = null)
    {
        Id = id;
        name = name;
    }
}

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    private readonly IExerciseRepository _repository;

    public DeleteExerciseCommandHandler(IMapper mapper, IMessagePublisher publisher, IExerciseRepository repository)
    {
        _mapper = mapper;
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<ApiResponse> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(request);
        await _repository.DeleteAsync(entity);

        var @event = _mapper.Map<ExerciseDeletedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new();
    }
}