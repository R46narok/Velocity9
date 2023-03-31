using FluentValidation;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreatePreferencesCommandValidator : AbstractValidator<CreatePreferencesCommand>
{
    public CreatePreferencesCommandValidator(IUserRepository userRepository, IPreferencesRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => (await userRepository.GetByNameAsync(name, false) is not null))
            .WithErrorCode("User does not exist");
        
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => (await repository.GetByUserNameAsync(name, false) is null))
            .WithErrorCode("Already exists");
    }
}