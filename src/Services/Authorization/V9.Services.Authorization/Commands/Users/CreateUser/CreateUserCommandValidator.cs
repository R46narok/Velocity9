using FluentValidation;
using Microsoft.AspNetCore.Identity;
using V9.Services.Authorization.Data.Entities;
using V9.Application;

namespace V9.Services.Authorization.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(u => u.UserName)
            .MustAsync(async (username, _) => await userManager.FindByNameAsync(username) is null)
            .WithErrorCode("User already exists");

        RuleFor(u => u.Email)
            .EmailAddress();
    }
}