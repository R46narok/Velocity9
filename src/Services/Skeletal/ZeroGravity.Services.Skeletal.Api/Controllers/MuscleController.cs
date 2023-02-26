using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Queries.GetAllMuscles;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

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
}