using FluentValidation;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Queires;

public class GetPreferencesQueryValidator : AbstractValidator<GetPreferencesQuery>
{
    public GetPreferencesQueryValidator(IPreferencesRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => (await repository.GetByUserNameAsync(name, false)) is not null)
            .WithErrorCode("Does not exist");
    }
}