using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExerciseAsync([FromBody] CreateExerciseCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
}