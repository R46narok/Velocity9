using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public class CreateFiberCommandValidator : AbstractValidator<CreateFiberCommand>
{
    public CreateFiberCommandValidator(IFiberRepository repository)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is null)
            .WithErrorCode("Fiber already exists");
    }
}