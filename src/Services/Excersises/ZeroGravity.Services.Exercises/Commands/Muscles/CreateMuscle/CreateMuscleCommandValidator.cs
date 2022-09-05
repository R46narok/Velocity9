using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Muscles.CreateMuscle;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator(IMuscleRepository repository)
    {
        RuleFor(x => x.Group)
            .MustAsync(async (group, _) => await repository.GetByGroupAsync(group, false) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Muscle group already exists.");

        RuleFor(x => x.Group)
            .NotNull()
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Invalid muscle group.");
        
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Invalid description.");

        RuleFor(x => x.HeadCount)
            .GreaterThan(0)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Invalid head count.");
    }
}