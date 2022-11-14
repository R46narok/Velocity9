using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Skeletal.Commands.CreateAuthor;

namespace ZeroGravity.Services.Skeletal.Handlers;

public class UserCreatedEvent : IDomainEvent
{
     public string Id { get; set; }
     public string UserName { get; set; }
     public string Email { get; set; }
     public string Phone { get; set; }
}

public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public UserCreatedEventHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateAuthorCommand>(domainEvent);
        await _mediator.Send(command, cancellationToken);
    }
}
