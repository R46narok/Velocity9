using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Commands;

public class CreateFiberCommandValidator : AbstractValidator<CreateFiberCommand>
{
    public CreateFiberCommandValidator(IFiberRepository repository)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is null)
            .WithErrorCode("Fiber already exists");
    }
}