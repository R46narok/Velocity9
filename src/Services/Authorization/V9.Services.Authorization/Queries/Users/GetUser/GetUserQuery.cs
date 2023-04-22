using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Services.Authorization.Dto;

namespace V9.Services.Authorization.Queries.Users.GetUser;

public record GetUserQuery(string UserName) : IRequest<ErrorOr<UserDto>>;

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