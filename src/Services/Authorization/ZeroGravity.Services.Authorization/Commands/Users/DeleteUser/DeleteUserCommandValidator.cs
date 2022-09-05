using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(UserManager<User> userManager)
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
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage("User does not exist in the database");
    }
}