using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Token.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => x)
            .MustAsync(async (command, _) =>
            {
                var user = await userManager.FindByNameAsync(command.UserName);
                return await userManager.CheckPasswordAsync(user, command.Password);
            })
            .WithErrorCode(StatusCode.Unauthorized)
            .WithMessage("Password is not correct");
    } 
}