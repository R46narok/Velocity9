using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Queries;
using ZeroGravity.Services.Skeletal.Queries.GetAllMuscleGroups;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class MuscleGroupController : ApiController
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
        return response.Match(Ok, Problem);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetMuscleGroupByNameAsync(string name)
    {
        var query = new GetMuscleGroupQuery(name);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
}