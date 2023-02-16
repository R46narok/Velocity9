using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record CreateWorkoutCommand(string WorkoutName, string UserName, string Notes, WorkoutType Type) : IRequest<PipelineResult>;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, PipelineResult>
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

    public async Task<PipelineResult> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Data.Entities.Workout>(request);
        entity.User = await _userRepository.GetByNameAsync(request.UserName);
        
        await _repository.CreateAsync(entity);
        
        entity = await _repository.GetByNameAsync(request.UserName, request.WorkoutName, false);
        var @event = _mapper.Map<WorkoutCreatedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
        
        return new("Created");
    }
}