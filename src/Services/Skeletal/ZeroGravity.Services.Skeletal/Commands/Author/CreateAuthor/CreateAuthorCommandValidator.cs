using FluentValidation;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator(IAuthorRepository repository)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name, false) is null);
        RuleFor(cmd => cmd.ExternalId)
            .MustAsync(async (id, _) => await repository.GetByExternalIdAsync(id, false) is null);
    }   
}