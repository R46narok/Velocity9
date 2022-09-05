using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Exercises.Commands.Exercises.CreateExercise;
using ZeroGravity.Services.Exercises.Commands.Exercises.DeleteExercise;
using ZeroGravity.Services.Exercises.Queries.Exercises.GetExercise;

namespace ZeroGravity.Services.Exercises.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ExerciseController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetExerciseById([FromQuery] int id)
    {
        var query = new GetExerciseQuery(id);
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExercise([FromQuery] int? id, [FromQuery] string? name)
    {
        var command = new DeleteExerciseCommand(id, name);
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
}