﻿using System.Security.Claims;
using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Services.Authorization.Dto;

namespace V9.Services.Authorization.Queries.Users.GetAllUsers;

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

        for (int i = 0; i < users.Count; ++i)
        {
            var claim = (await _userManager.GetClaimsAsync(users[i]))
                .SingleOrDefault(x => x.Type is ClaimTypes.Role);

            dtos[i].Role = claim.Value;
        }

        return dtos;
    }
}

