using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Commands;

public class CreateMuscleGroupCommandValidator : AbstractValidator<CreateMuscleGroupCommand>
{
    public CreateMuscleGroupCommandValidator(IMuscleGroupRepository repository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Already exists");

        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest);

        RuleFor(cmd => cmd.Description)
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest);
    }
}