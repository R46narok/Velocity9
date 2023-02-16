using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record DeleteSetCommand(string WorkoutName, string UserName, int Index) : IRequest<PipelineResult>;

public class DeleteSetCommandHandler : IRequestHandler<DeleteSetCommand, PipelineResult>
{
    private readonly ISetRepository _setRepository;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;

    public DeleteSetCommandHandler(ISetRepository setRepository, IMessagePublisher publisher, IMapper mapper)
    {
        _setRepository = setRepository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<PipelineResult> Handle(DeleteSetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _setRepository.GetByIndexAsync(request.UserName, request.WorkoutName, request.Index);
        var @event = _mapper.Map<SetDeletedEvent>(entity);
        
        await _setRepository.DeleteAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new();
    }
}