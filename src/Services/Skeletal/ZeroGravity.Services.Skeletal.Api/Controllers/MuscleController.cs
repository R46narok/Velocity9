using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Skeletal.Queries.GetAllMuscles;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class MuscleController : ControllerBase
{
    private readonly IMediator _mediator;

    public MuscleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMusclesAsync()
    {
        var query = new GetAllMusclesQuery();
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }
}