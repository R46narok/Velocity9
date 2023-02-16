using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class DeleteSetCommandValidator : AbstractValidator<DeleteSetCommand>
{
    public DeleteSetCommandValidator(ISetRepository repository)
    {
        RuleFor(cmd => new {cmd.UserName, cmd.WorkoutName, cmd.Index})
            .MustAsync(async (prop, _) =>
                await repository.GetByIndexAsync(prop.UserName, prop.WorkoutName, prop.Index, false) is not null)
            .WithMessage("Set not present in the database")
            .WithErrorCode(StatusCode.NotFound);
    }
}