using MediatR;
using Microsoft.AspNetCore.Mvc;
using V9.Application;
using V9.Services.Skeletal.Queries.GetAllMuscles;
using V9.Services.Skeletal.Queries.GetMuscleImage;

namespace V9.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class MuscleController : ApiController
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
        return response.Match(Ok, Problem);
    }

    [HttpGet("/api/[controller]/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMuscleImageByNameAsync([FromQuery] string name)
    {
        var query = new GetMuscleImageQuery(name);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
}