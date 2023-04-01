using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;
using V9.Services.Skeletal.Queries.GetAllMuscleGroups;

namespace V9.Services.Skeletal.Queries;

public class GetMuscleGroupQueryValidator : AbstractValidator<GetMuscleGroupQuery>
{
    public GetMuscleGroupQueryValidator(IMuscleGroupRepository repository)
    {
        RuleFor(query => query.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Muscle group does not exist in the database");
    }
}