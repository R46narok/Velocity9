using FluentValidation;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Queires;

public class GetPreferencesQueryValidator : AbstractValidator<GetPreferencesQuery>
{
    public GetPreferencesQueryValidator(IPreferencesRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => (await repository.GetByUserNameAsync(name, false)) is not null)
            .WithErrorCode("Does not exist");
    }
}