using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using V9.Application;
using V9.Services.Authorization.Commands.Users.CreateUser;
using V9.Services.Authorization.Commands.Users.DeleteUser;
using V9.Services.Authorization.Commands.Users.ElevateUser;
using V9.Services.Authorization.Queries.Users.GetAllUsers;
using V9.Services.Authorization.Queries.Users.GetUser;

namespace V9.Services.Authorization.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class UserController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromQuery] string username)
    {
        var query = new GetUserQuery(username);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("/api/[controller]/all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllUsersQuery();
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpPut]
    public async Task<IActionResult> ElevateAsync([FromQuery] string? id, [FromQuery] string? username)
    {
        var command = new ElevateUserCommand(id, username);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}