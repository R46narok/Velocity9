using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record CreateWorkoutCommandResponse(int Id);

public class CreateWorkoutCommand : IRequest<ErrorOr<CreateWorkoutCommandResponse>>
{
    public string? WorkoutName { get; set; }
    public string UserName { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }
}

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, ErrorOr<CreateWorkoutCommandResponse>>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    private readonly IUserRepository _userRepository;

    public CreateWorkoutCommandHandler(IWorkoutRepository repository, IMapper mapper, IMessagePublisher publisher, IUserRepository userRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _publisher = publisher;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CreateWorkoutCommandResponse>> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Data.Entities.Workout>(request);
        entity.User = await _userRepository.GetByNameAsync(request.UserName);
        
        var id = await _repository.CreateAsync(entity);
        
        entity = await _repository.GetByNameAsync(request.UserName, request.WorkoutName, false);
        var @event = _mapper.Map<WorkoutCreatedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
        
        return new CreateWorkoutCommandResponse(id);
    }
}