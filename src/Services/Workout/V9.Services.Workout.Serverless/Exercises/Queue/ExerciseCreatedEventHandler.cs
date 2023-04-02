using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using V9.Domain.Events;
using V9.Services.Workout.Commands;

namespace V9.Services.Workout.Serverless.Exercises.Queue;

public class ExerciseCreatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}

public class ExerciseCreatedEventHandler
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ExerciseCreatedEventHandler> _logger;

    public ExerciseCreatedEventHandler(IMediator mediator, IMapper mapper, ILogger<ExerciseCreatedEventHandler> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [Function("ExerciseCreatedEventHandler")]
    public async Task Run(
    [ServiceBusTrigger(nameof(ExerciseCreatedEvent), "coach", Connection = "AzureServiceBus")] 
    ExerciseCreatedEvent domainEvent,
        FunctionContext context)
    {
        var command = _mapper.Map<CreateExerciseCommand>(domainEvent);
        var result = await _mediator.Send(command);

        result.Switch(
            value => _logger.LogInformation("Exercise {Id} created from Azure Service Bus", value.Id),
            errors => _logger.LogError("Failed {Errors}", errors)
        );
    }
}