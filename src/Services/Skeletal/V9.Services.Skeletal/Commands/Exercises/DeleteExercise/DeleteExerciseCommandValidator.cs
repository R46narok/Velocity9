using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Commands.Exercises.DeleteExercise;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
{
    public DeleteExerciseCommandValidator(IExerciseRepository repository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Exercise does not exist in the database");
    }
}