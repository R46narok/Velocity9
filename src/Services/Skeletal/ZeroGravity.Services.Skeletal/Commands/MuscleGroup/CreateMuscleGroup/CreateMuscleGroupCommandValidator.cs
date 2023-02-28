using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public class CreateMuscleGroupCommandValidator : AbstractValidator<CreateMuscleGroupCommand>
{
    public CreateMuscleGroupCommandValidator(IMuscleGroupRepository repository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is null)
            .WithMessage("Already exists");

        RuleFor(cmd => cmd.Name)
            .NotEmpty();

        RuleFor(cmd => cmd.Description)
            .NotEmpty();
    }
}