using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Muscles.Data.Repositories;
using ZeroGravity.Services.Muscles.Queries.GetAllMuscleGroups;

namespace ZeroGravity.Services.Muscles.Queries;

public class GetMuscleGroupQueryValidator : AbstractValidator<GetMuscleGroupQuery>
{
    public GetMuscleGroupQueryValidator(IMuscleGroupRepository repository)
    {
        RuleFor(query => query.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage(DetailsMessage.For(StatusCode.NotFound, nameof(GetMuscleGroupQuery.Name)));
    }
}