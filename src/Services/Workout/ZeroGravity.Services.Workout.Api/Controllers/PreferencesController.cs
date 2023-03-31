﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Queires;

namespace ZeroGravity.Services.Workout.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class PreferencesController : ApiController
{
    private readonly IMediator _mediator;

    public PreferencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByUserNameAsync()
    {
        var userName = User.Identity!.Name!;
        var query = new GetPreferencesQuery {UserName = userName};
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePreferencesCommand command)
    {
        var userName = User.Identity!.Name!;
        command.UserName = userName;
        
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}