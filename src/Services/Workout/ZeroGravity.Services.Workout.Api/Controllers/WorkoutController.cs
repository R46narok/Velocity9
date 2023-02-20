using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Commands.PredictWorkout;
using ZeroGravity.Services.Workout.Queires;

namespace ZeroGravity.Services.Workout.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [EndpointName("Create a new workout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWorkoutAsync([FromBody] CreateWorkoutCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkoutAsync([FromQuery] string userName, [FromQuery] string workoutName)
    {
        var query = new GetWorkoutQuery(userName, workoutName);
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }
    
    
    [HttpDelete]
    public async Task<IActionResult> DeleteWorkoutAsync([FromQuery] string userName, [FromQuery] string workoutName)
    {
        var command = new DeleteWorkoutCommand(workoutName, userName);
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }

    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PredictWorkoutAsync([FromQuery] string userName)
    {
        var command = new PredictWorkoutCommand(userName);
        var predicted = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(predicted);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkoutAsync([FromBody] UpdateWorkoutCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
    
}