using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public record DeleteSetCommandResponse(int Index);
public record DeleteSetCommand(string WorkoutName, string UserName, int Index) : IRequest<ErrorOr<DeleteSetCommandResponse>>;

public class DeleteSetCommandHandler : IRequestHandler<DeleteSetCommand, ErrorOr<DeleteSetCommandResponse>>
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

    public async Task<ErrorOr<DeleteSetCommandResponse>> Handle(DeleteSetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _setRepository.GetByIndexAsync(request.UserName, request.WorkoutName, request.Index);
        var @event = _mapper.Map<SetDeletedEvent>(entity);
        
        await _setRepository.DeleteAsync(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new DeleteSetCommandResponse(request.Index);
    }
}