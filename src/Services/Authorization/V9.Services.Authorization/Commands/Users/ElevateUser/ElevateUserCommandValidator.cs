using FluentValidation;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Application;

namespace V9.Services.Authorization.Commands.Users.ElevateUser;

public class ElevateUserCommandValidator : AbstractValidator<ElevateUserCommand>
{
     public ElevateUserCommandValidator(UserManager<User> userManager)
     {
         RuleFor(x => x)
             .MustAsync(async (command, _) =>
             {
                 if (command.UserName is not null)
                     return await userManager.FindByNameAsync(command.UserName) is not null;
                 if (command.Id is not null)
                     return await userManager.FindByIdAsync(command.Id) is not null;

                 return false;
             })
             .WithName("UserName")
             .WithErrorCode("Not authorized");
     }
}