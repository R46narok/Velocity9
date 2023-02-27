using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Services.Authorization.Data.Entities;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Queries.Users.GetAllUsers;

public record GetAllUsersQuery : IRequest<ErrorOr<List<UserDto>>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<List<UserDto>>>
    {
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.ToList();
        var dtos = users
            .Select(x => _mapper.Map<UserDto>(x))
            .ToList();

        return dtos;
    }
}

