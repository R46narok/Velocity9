using FluentValidation;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is null);
        RuleFor(cmd => cmd.ExternalId)
            .MustAsync(async (id, _) => await repository.GetByExternalIdAsync(id, false) is null);
    }   
}