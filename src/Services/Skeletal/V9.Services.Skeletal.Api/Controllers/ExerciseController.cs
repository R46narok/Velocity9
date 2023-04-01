using MediatR;
using Microsoft.AspNetCore.Mvc;
using V9.Application;
using V9.Services.Skeletal.Commands.Exercises.CreateExercise;
using V9.Services.Skeletal.Commands.Exercises.DeleteExercise;
using V9.Services.Skeletal.Commands.Exercises.UpdateExercise;
using V9.Services.Skeletal.Queries.GetAllExercises;

namespace V9.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ExerciseController : ApiController
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
        return response.Match(Ok, Problem);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateExerciseAsync([FromBody] CreateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateExerciseAsync([FromBody] UpdateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExerciseAsync([FromBody] DeleteExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}