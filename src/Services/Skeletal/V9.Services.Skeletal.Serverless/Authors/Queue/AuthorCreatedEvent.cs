﻿using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using V9.Domain.Events;
using V9.Services.Skeletal.Commands.CreateAuthor;

namespace V9.Services.Skeletal.Serverless.Authors.Queue;

public class UserCreatedEvent : IDomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class UserCreatedEventHandler
{
    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(IMediator mediator, IMapper mapper, ILogger<UserCreatedEventHandler> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    [Function("UserCreatedEventHandler")]
    public async Task Run(
        [ServiceBusTrigger(nameof(UserCreatedEvent), "skeletal", Connection = "AzureServiceBus")] 
        UserCreatedEvent domainEvent,
        FunctionContext context)
    {
        var command = _mapper.Map<CreateAuthorCommand>(domainEvent);
        var result = await _mediator.Send(command);

        result.Switch(
            value => _logger.LogInformation("Author {Id} created from Azure Service Bus", value.ExternalId),
            errors => _logger.LogError("Failed {Errors}", errors)
        );
    }
}