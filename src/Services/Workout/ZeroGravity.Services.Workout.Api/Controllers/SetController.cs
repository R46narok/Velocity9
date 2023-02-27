using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Commands;

namespace ZeroGravity.Services.Workout.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SetController : ApiController
{
    private readonly IMediator _mediator;

    public SetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSetAsync([FromBody] CreateSetCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
    
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSetAsync([FromQuery] string userName, [FromQuery] string workoutName, [FromQuery] int index)
    {
        var command = new DeleteSetCommand(workoutName, userName, index);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSetAsync([FromBody] UpdateSetCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}