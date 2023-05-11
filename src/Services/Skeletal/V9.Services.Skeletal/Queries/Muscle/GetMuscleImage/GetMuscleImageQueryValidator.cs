using FluentValidation;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Queries.GetMuscleImage;

public class GetMuscleImageQueryValidator : AbstractValidator<GetMuscleImageQuery>
{
    public GetMuscleImageQueryValidator(IMuscleRepository repository)
    {
        RuleFor(q => q.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Muscle does not exist in the database");
    }
}