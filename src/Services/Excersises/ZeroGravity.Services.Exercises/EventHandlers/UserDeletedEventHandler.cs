using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Exercises.Commands.Authors.DeleteAuthor;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.EventHandlers;

public class UserDeletedEvent : IDomainEvent
{
     public string Id { get; set; }
     public string UserName { get; set; }
}

public class UserDeletedEventHandler : IDomainEventHandler<UserDeletedEvent>
{
     private readonly IMediator _mediator;
     private readonly IMapper _mapper;
     
     public UserDeletedEventHandler(IMediator mediator, IMapper mapper)
     {
         _mediator = mediator;
         _mapper = mapper;
     }

     public async Task HandleAsync(UserDeletedEvent domainEvent, CancellationToken cancellationToken = default)
     {
         var command = _mapper.Map<DeleteAuthorCommand>(domainEvent);
         await _mediator.Send(command, cancellationToken);
     }
}