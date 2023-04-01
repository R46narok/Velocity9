using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Commands;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator(IMuscleRepository muscleRepository, IMuscleGroupRepository muscleGroupRepository)
    {
        RuleFor(cmd => cmd.Group)
            .MustAsync(async (group, _) => await muscleGroupRepository.GetByNameAsync(group) is not null)
            .WithErrorCode("Muscle group does not exist");

        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await muscleRepository.GetByNameAsync(name) is null)
            .WithErrorCode("Already exists");
    }
}