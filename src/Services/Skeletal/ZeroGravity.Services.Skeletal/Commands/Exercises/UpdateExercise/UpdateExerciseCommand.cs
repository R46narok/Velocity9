using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.UpdateExercise;

public record UpdateExerciseCommand(int Id, string? Name, string? Description) : IRequest<PipelineResult>;

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;
    private readonly IMessagePublisher _publisher;

    public UpdateExerciseCommandHandler(
        IMapper mapper,
        IExerciseRepository repository,
        IMessagePublisher publisher)
    {
        _mapper = mapper;
        _repository = repository;
        _publisher = publisher;
    }
    
    public async Task<PipelineResult> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _repository.GetByIdAsync(request.Id))!;
        
        entity.Name = request.Name ?? entity.Name;
        entity.Description = request.Description ?? entity.Description;

        var @event = _mapper.Map<ExerciseUpdatedEvent>(entity);

        await _repository.UpdateAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
        
        return new();
    }
}