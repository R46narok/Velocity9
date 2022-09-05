using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Exercises.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator(IAuthorRepository authorRepository, IExerciseRepository exerciseRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Exercise name is not valid");

        RuleFor(x => x.Name)
            .MustAsync(async (name, _) => await exerciseRepository.GetByNameAsync(name, false) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Exercise already exists");

        // RuleFor(x => x.TargetMuscles)
            // .NotEmpty()
            // .WithErrorCode(StatusCode.BadRequest)
            // .WithMessage("Target muscles cannot be empty");

        RuleFor(x => x.AuthorName)
            .MustAsync(async (author, _) => await authorRepository.GetByUserNameAsync(author, false) is not null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Author does not exist");
    }
}