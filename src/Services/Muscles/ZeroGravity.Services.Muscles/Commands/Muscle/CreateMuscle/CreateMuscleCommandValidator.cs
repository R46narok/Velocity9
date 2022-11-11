using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Commands;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator(IMuscleRepository muscleRepository, IMuscleGroupRepository muscleGroupRepository)
    {
        RuleFor(cmd => cmd.Group)
            .MustAsync(async (group, _) => await muscleGroupRepository.GetByNameAsync(group) is not null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Muscle group does not exist");

        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await muscleRepository.GetByNameAsync(name) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Already exists");
    }
}