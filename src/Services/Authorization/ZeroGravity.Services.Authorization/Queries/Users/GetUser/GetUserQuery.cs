using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Services.Authorization.Data.Entities;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Queries.Users.GetUser;

public class GetUserQuery : IRequest<ErrorOr<UserDto>>
{
    public string UserName { get; set; }
    
    public GetUserQuery(string userName)
    {
        UserName = userName;
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GetUserQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ErrorOr<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var dto = _mapper.Map<UserDto>(user);

        return dto;
    }
}