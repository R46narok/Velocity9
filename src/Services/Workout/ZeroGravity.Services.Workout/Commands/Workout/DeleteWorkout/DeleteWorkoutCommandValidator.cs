using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class DeleteWorkoutCommandValidator : AbstractValidator<DeleteWorkoutCommand>
{
    public DeleteWorkoutCommandValidator(IWorkoutRepository repository)
    {
        RuleFor(cmd => new {cmd.UserName, cmd.WorkoutName})
            .MustAsync(async (prop, _) =>
                await repository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithMessage("Workout not present in the database")
            .WithErrorCode(StatusCode.NotFound);
    }
}