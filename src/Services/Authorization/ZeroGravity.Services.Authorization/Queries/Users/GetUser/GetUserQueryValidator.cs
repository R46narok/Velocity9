using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Queries.Users.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.UserName)
            .MustAsync(async (username, _) => await userManager.FindByNameAsync(username) is not null)
            .WithErrorCode("User does not exist in the database");
    }
}