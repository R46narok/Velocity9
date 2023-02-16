using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Authorization.Data.Entities;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Queries.Users.GetUser;

public class GetUserQuery : IRequest<PipelineResult<UserDto>>
{
    public string UserName { get; set; }
    
    public GetUserQuery(string userName)
    {
        UserName = userName;
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, PipelineResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GetUserQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<PipelineResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var dto = _mapper.Map<UserDto>(user);

        return new(dto);
    }
}