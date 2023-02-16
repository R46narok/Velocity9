using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class UpdateWorkoutCommandValidator : AbstractValidator<UpdateWorkoutCommand>
{
    public UpdateWorkoutCommandValidator(IWorkoutRepository repository)
    {
        RuleFor(cmd => new {cmd.UserName, cmd.WorkoutName})
            .MustAsync(async (prop, _) =>
                await repository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithMessage("Workout is not present in the database")
            .WithErrorCode(StatusCode.NotFound);
        
        RuleFor(cmd => new {cmd.UserName, cmd.NewWorkoutName})
            .MustAsync(async (prop, _) =>
                await repository.GetByNameAsync(prop.UserName, prop.NewWorkoutName, false) is null)
            .WithMessage("Workout is already present in the database")
            .WithErrorCode(StatusCode.BadRequest);
    }
}