using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Authors.DeleteAuthor;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator(IAuthorRepository repository)
    {
        RuleFor(x => x)
            .MustAsync(async (command, _) =>
            {
                if (command.UserName is not null)
                    return await repository.GetByUserNameAsync(command.UserName) is not null;
                if (command.ExternalId is not null)
                    return await repository.GetByExternalIdAsync(command.ExternalId) is not null;
        
                return false;
            })
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage("Author does not exist in the database");
    }
}