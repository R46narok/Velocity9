using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public record UpdateSetCommandResponse(int Index);
public record UpdateSetCommand
    (string? Notes, int? CompletedReps, int Index, string WorkoutName, string UserName) 
    : IRequest<ErrorOr<UpdateSetCommandResponse>>;

public class UpdateSetCommandHandler : IRequestHandler<UpdateSetCommand, ErrorOr<UpdateSetCommandResponse>>
{
    private readonly ISetRepository _setRepository;
    private readonly IMessagePublisher _publisher;
    private readonly IMapper _mapper;

    public UpdateSetCommandHandler(ISetRepository setRepository, IMessagePublisher publisher, IMapper mapper)
    {
        _setRepository = setRepository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UpdateSetCommandResponse>> Handle(UpdateSetCommand request, CancellationToken cancellationToken)
    {
        var entity = (await _setRepository.GetByIndexAsync(request.UserName, request.WorkoutName, request.Index))!;
        
        entity.Notes = request.Notes ?? entity.Notes;
        entity.CompletedReps = request.CompletedReps ?? entity.CompletedReps;

        await _setRepository.UpdateAsync(entity);

        return new UpdateSetCommandResponse(request.Index);
    }
}
