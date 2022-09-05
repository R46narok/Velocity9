using AutoMapper;
using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Muscles.CreateMuscle;

public class CreateMuscleCommand : IRequest<ApiResponse>
{

    public string Group { get; set; }
    public string Description { get; set; }
    public int HeadCount { get; set; }
    
    public CreateMuscleCommand(string group, string description, int headCount)
    {
        Group = group;
        Description = description;
        HeadCount = headCount;
    }
}

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    private readonly IMuscleRepository _repository;

    public CreateMuscleCommandHandler(IMapper mapper, IMessagePublisher publisher, IMuscleRepository repository)
    {
        _mapper = mapper;
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<ApiResponse> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Muscle>(request);
        await _repository.CreateAsync(entity);

        var @event = _mapper.Map<MuscleCreatedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

        return new(statusCode: StatusCode.Ok, 
            detail: DetailsMessage.For(StatusCode.Created, nameof(Muscle)));
    }
}