﻿using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;

namespace ZeroGravity.Services.Workout.Handlers;

public class ExerciseCreatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}


public class ExerciseCreatedEventHandler : IDomainEventHandler<ExerciseCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public ExerciseCreatedEventHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task HandleAsync(ExerciseCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateExerciseCommand>(domainEvent);
        var response = await _mediator.Send(command, cancellationToken);
    }
}
