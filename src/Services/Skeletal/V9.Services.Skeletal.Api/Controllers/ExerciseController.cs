using MediatR;
using Microsoft.AspNetCore.Mvc;
using V9.Application;
using V9.Services.Skeletal.Commands.Exercises.CreateExercise;
using V9.Services.Skeletal.Commands.Exercises.DeleteExercise;
using V9.Services.Skeletal.Commands.Exercises.UpdateExercise;
using V9.Services.Skeletal.Data.Repositories;
using V9.Services.Skeletal.Queries;
using V9.Services.Skeletal.Queries.GetAllExercises;

namespace V9.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ExerciseController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IExerciseRepository _repository;

    public ExerciseController(IMediator mediator, IExerciseRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllExercisesAsync()
    {
        var query = new GetAllExercisesQuery();
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("/api/exercise/details")]
    public async Task<IActionResult> GetExerciseByNameAsync([FromQuery] string name)
    {
        var query = new GetExerciseQuery(name);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("/api/exercise/thumbnail")]
    public async Task<IActionResult> GetExerciseThumbnailAsync([FromQuery] string name)
    {
        var entry = await _repository.GetByNameAsync(name, false);
        return new FileContentResult(entry.Thumbnail, "image/jpeg");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequestSizeLimit(100_000_000)]
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