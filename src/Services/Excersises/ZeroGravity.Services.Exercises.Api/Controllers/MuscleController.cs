using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Services.Exercises.Commands.Muscles.CreateMuscle;
using ZeroGravity.Services.Exercises.Data.Repositories;
using ZeroGravity.Services.Exercises.Queries.Muscles.GetAllMuscles;
using ZeroGravity.Services.Exercises.Queries.Muscles.GetMuscle;

namespace ZeroGravity.Services.Exercises.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class MuscleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IMuscleRepository _repository;

    public MuscleController(IMapper mapper, IMediator mediator, IMuscleRepository repository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllMusclesQuery();
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMuscleAsync([FromQuery] int? id, [FromQuery] string? group)
    {
        var query = new GetMuscleQuery(id, group);
        var response = await _mediator.Send(query);
        return Application.StatusCode.ToObjectResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMuscleAsync([FromBody] CreateMuscleCommand command)
    {
        var response = await _mediator.Send(command);
        return Application.StatusCode.ToObjectResult(response);
    }
}