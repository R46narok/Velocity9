using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Muscles.Queries;
using ZeroGravity.Services.Muscles.Queries.GetAllFibers;

namespace ZeroGravity.Services.Muscles.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class FiberController : ControllerBase
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
        var result = await _mediator.Send(query);

        return Application.StatusCode.ToObjectResult(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetFiberByNameAsync(string name)
    {
        var query = new GetFiberQuery(name);
        var result = await _mediator.Send(query);

        return Application.StatusCode.ToObjectResult(result);
    }
}