using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Authorization.Commands.Users.CreateUser;
using ZeroGravity.Services.Authorization.Commands.Users.DeleteUser;
using ZeroGravity.Services.Authorization.Commands.Users.ElevateUser;
using ZeroGravity.Services.Authorization.Queries.Users.GetUser;

namespace ZeroGravity.Services.Authorization.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class UserController : ControllerBase
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
        
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromQuery] string username)
    {
        var query = new GetUserQuery(username);
        var response = await _mediator.Send(query);
        
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> ElevateAsync([FromQuery] string? id, [FromQuery] string? username)
    {
        var command = new ElevateUserCommand(id, username);
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
}