using FluentValidation;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands.PredictWorkout;

public class PredictWorkoutCommandValidator : AbstractValidator<PredictWorkoutCommand>
{
    public PredictWorkoutCommandValidator(IUserRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is not null)
            .WithErrorCode("User not present in the database");
    }
}