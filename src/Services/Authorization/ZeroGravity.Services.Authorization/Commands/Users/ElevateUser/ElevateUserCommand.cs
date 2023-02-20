using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Users.ElevateUser;

public class ElevateUserCommand : IRequest<CqrsResult>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }

    public ElevateUserCommand(string? id = null, string? userName = null)
    {
        Id = id;
        UserName = userName;
    }
}

public class ElevateUserCommandHandler : IRequestHandler<ElevateUserCommand, CqrsResult>
{
    private readonly UserManager<User> _userManager;

    public ElevateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CqrsResult> Handle(ElevateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        
        await _userManager.ReplaceClaimAsync(user, 
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, "Admin"));
        return new(statusCode: StatusCode.Ok);
    }
    
     private async Task<User> FindUserByNameOrId(ElevateUserCommand command)
     {
         if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
         return await _userManager.FindByIdAsync(command.Id);
     }
}