using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using V9.Application;
using V9.Services.Authorization.Commands.Token.CreateToken;
using V9.Services.Authorization.Dto;

namespace V9.Services.Authorization.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class TokenController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TokenController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateToken([FromBody] UserCredentialsDto credentials)
    {
        var command = _mapper.Map<CreateTokenCommand>(credentials);
        var response = await _mediator.Send(command);

        return response.Match(Ok, Problem);
    }
}