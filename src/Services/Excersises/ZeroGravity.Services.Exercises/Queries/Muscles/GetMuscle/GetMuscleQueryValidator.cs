using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Queries.Muscles.GetMuscle;

public class GetMuscleQueryValidator : AbstractValidator<GetMuscleQuery>
{
    public GetMuscleQueryValidator(IMuscleRepository repository)
    {
        RuleFor(x => x)
            .MustAsync(async (muscle, _) =>
            {
                if (muscle.Group is not null)
                    return await repository.GetByGroupAsync(muscle.Group, false) is not null;
                if (muscle.Id is not null)
                    return await repository.GetByIdAsync(muscle.Id.Value, false) is not null;

                return false;
            })
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage(DetailsMessage.For(StatusCode.NotFound, nameof(Muscle)));
    }
}