using FluentValidation;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Queries;

public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator(IExerciseRepository repository)
    {
        RuleFor(q => q.Name)
            .NotNull()
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Exercise does not exist ìn the database");
    }
}