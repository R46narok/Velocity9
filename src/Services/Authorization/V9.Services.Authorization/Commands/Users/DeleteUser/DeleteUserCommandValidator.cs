using FluentValidation;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Application;

namespace V9.Services.Authorization.Commands.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => new {x.Id, x.UserName})
            .MustAsync(async (command, _) =>
            {
                if (command.UserName is not null)
                    return await userManager.FindByNameAsync(command.UserName) is not null;
                if (command.Id is not null)
                    return await userManager.FindByIdAsync(command.Id) is not null;

                return false;
            })
            .WithName("UserName")
            .WithErrorCode("User does not exist in the database");
    }
}