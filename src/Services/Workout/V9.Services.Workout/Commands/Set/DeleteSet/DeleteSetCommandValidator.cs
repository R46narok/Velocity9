using FluentValidation;
using V9.Application;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public class DeleteSetCommandValidator : AbstractValidator<DeleteSetCommand>
{
    public DeleteSetCommandValidator(ISetRepository repository)
    {
        RuleFor(cmd => new {cmd.UserName, cmd.WorkoutName, cmd.Index})
            .MustAsync(async (prop, _) =>
                await repository.GetByIndexAsync(prop.UserName, prop.WorkoutName, prop.Index, false) is not null)
            .WithName("Set")
            .WithErrorCode("Set not present in the database");
    }
}