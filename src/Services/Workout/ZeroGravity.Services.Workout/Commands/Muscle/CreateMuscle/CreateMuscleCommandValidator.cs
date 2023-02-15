using FluentValidation;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty();
    }
}