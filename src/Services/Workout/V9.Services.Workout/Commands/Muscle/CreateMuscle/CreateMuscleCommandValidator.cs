using FluentValidation;
using V9.Application;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator(IMuscleRepository muscleRepository)
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty();

        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await muscleRepository.GetByNameAsync(name) is null)
            .WithErrorCode("Already exists");
    }
}