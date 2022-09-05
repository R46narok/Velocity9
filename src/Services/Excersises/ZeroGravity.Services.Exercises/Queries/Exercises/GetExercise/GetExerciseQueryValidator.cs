using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Queries.Exercises.GetExercise;

public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator(IExerciseRepository repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id, false) is not null)
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage("Exercise not found");
    }
}