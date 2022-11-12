using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Muscles.Queries;
using ZeroGravity.Services.Muscles.Queries.GetAllMuscleGroups;

namespace ZeroGravity.Services.Muscles.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class MuscleGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public MuscleGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMuscleGroupsAsync()
    {
        var query = new GetAllMuscleGroupsQuery();
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetMuscleGroupByNameAsync(string name)
    {
        var query = new GetMuscleGroupQuery(name);
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }
}