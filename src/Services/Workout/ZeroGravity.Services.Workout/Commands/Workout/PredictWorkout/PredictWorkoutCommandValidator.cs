using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands.PredictWorkout;

public class PredictWorkoutCommandValidator : AbstractValidator<PredictWorkoutCommand>
{
    public PredictWorkoutCommandValidator(IUserRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is not null)
            .WithMessage("User not present in the database")
            .WithErrorCode(StatusCode.NotFound);
    }
}