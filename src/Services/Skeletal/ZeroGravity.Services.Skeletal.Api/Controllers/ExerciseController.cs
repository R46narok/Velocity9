using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;
using ZeroGravity.Services.Skeletal.Commands.Exercises.DeleteExercise;
using ZeroGravity.Services.Skeletal.Commands.Exercises.UpdateExercise;
using ZeroGravity.Services.Skeletal.Queries.GetAllExercises;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllExercisesAsync()
    {
        var query = new GetAllExercisesQuery();
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateExerciseAsync([FromBody] CreateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateExerciseAsync([FromBody] UpdateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExerciseAsync([FromBody] DeleteExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
}