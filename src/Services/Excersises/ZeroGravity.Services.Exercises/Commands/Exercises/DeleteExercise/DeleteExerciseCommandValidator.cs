using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Exercises.DeleteExercise;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
{
    public DeleteExerciseCommandValidator(IExerciseRepository repository)
    {
        RuleFor(x => x)
            .MustAsync(async (command, _) =>
            {
                if (command.Id is not null)
                    return await repository.GetByIdAsync(command.Id.Value, false) is not null;
                if (command.Name is not null)
                    return await repository.GetByNameAsync(command.Name, false) is not null;

                return false;
            })
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage("Exercise not found");
    }
}