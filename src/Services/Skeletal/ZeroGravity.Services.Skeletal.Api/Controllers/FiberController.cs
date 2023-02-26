using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Queries;
using ZeroGravity.Services.Skeletal.Queries.GetAllFibers;

namespace ZeroGravity.Services.Skeletal.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class FiberController : ApiController
{
    private readonly IMediator _mediator;

    public FiberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFibersAsync()
    {
        var query = new GetAllFibersQuery();
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetFiberByNameAsync(string name)
    {
        var query = new GetFiberQuery(name);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
}