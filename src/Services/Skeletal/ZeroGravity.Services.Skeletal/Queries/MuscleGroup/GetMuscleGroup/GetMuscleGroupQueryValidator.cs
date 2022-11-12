using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Queries.GetAllMuscleGroups;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Queries;

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