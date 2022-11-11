using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Commands;

public class CreateFiberCommandValidator : AbstractValidator<CreateFiberCommand>
{
    public CreateFiberCommandValidator(IFiberRepository repository)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Fiber already exists");
    }
}