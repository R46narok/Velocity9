using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using V9.Application;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Commands.PredictWorkout;
using V9.Services.Workout.Queires;

namespace V9.Services.Workout.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WorkoutController : ApiController
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWorkoutAsync([FromBody] CreateWorkoutCommand command)
    {
        var userName = User.Identity!.Name!;
        if (string.IsNullOrEmpty(command.UserName))
            command.UserName = userName;
        
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetWorkoutAsync([FromQuery] string workoutName)
    {
        var userName = User.Identity!.Name!;
        var query = new GetWorkoutQuery(userName, workoutName);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("/api/[controller]/all")]
    public async Task<IActionResult> GetAllWorkouts()
    {
        var userName = User.Identity!.Name!;
        var query = new GetAllWorkoutsQuery(userName);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteWorkoutAsync([FromQuery] string workoutName)
    {
        var userName = User.Identity!.Name!;
        var command = new DeleteWorkoutCommand(workoutName, userName);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PredictWorkoutAsync()
    {
        var userName = User.Identity!.Name!;
        var command = new PredictWorkoutCommand(userName);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkoutAsync([FromBody] UpdateWorkoutCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}