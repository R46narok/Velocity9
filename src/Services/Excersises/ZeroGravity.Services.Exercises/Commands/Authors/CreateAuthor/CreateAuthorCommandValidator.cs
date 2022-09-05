using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Authors.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator(IAuthorRepository repository)
    {
        RuleFor(x => x.UserName)
            .MustAsync(async (username, _) => await repository.GetByUserNameAsync(username) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Author already exists");
    } 
}