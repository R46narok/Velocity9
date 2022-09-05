using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(u => u.UserName)
            .MustAsync(async (username, _) => await userManager.FindByNameAsync(username) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("User already exists");

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Email cannot be empty");
    }
}