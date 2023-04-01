using FluentValidation;
using V9.Application;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public class DeleteWorkoutCommandValidator : AbstractValidator<DeleteWorkoutCommand>
{
    public DeleteWorkoutCommandValidator(IWorkoutRepository repository)
    {
        RuleFor(cmd => new {cmd.UserName, cmd.WorkoutName})
            .MustAsync(async (prop, _) =>
                await repository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithName("Workout")
            .WithErrorCode("Workout not present in the database");
    }
}