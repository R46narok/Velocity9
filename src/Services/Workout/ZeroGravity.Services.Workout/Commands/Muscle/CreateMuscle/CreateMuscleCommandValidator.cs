using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator(IMuscleRepository muscleRepository)
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty();

        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await muscleRepository.GetByNameAsync(name) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Already exists");
    }
}