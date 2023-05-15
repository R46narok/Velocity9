using System.Security.Claims;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Application;

namespace V9.Services.Authorization.Commands.Users.ElevateUser;

public class ElevateUserCommand : IRequest<ErrorOr<string>>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string Role { get; set; }

    public ElevateUserCommand(string role, string? id = null, string? userName = null)
    {
        Id = id;
        UserName = userName;
        Role = role;
    }
}

public class ElevateUserCommandHandler : IRequestHandler<ElevateUserCommand, ErrorOr<string>>
{
    private readonly UserManager<User> _userManager;

    public ElevateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<string>> Handle(ElevateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);

        var claims = await _userManager.GetClaimsAsync(user);
        var roleClaim = claims.SingleOrDefault(x => x.Type == ClaimTypes.Role);

        await _userManager.RemoveClaimAsync(user, roleClaim);
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));
        return request.UserName;
    }
    
     private async Task<User> FindUserByNameOrId(ElevateUserCommand command)
     {
         if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
         return await _userManager.FindByIdAsync(command.Id);
     }
}