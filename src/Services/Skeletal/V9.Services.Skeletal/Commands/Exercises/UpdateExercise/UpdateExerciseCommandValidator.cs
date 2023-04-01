using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Commands.Exercises.UpdateExercise;

public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator(IExerciseRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithErrorCode("Exercise does not exist in the database");
    }
}