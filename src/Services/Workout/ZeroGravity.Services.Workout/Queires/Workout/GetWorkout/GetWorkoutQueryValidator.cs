using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;
using ZeroGravity.Services.Workout.Queires;

namespace ZeroGravity.Services.Workout.Queries;

public class GetWorkoutQueryValidator : AbstractValidator<GetWorkoutQuery>
{
    public GetWorkoutQueryValidator(IWorkoutRepository workoutRepository)
    {
        RuleFor(query => new {query.UserName, query.WorkoutName})
            .MustAsync(async (prop, _) =>
                await workoutRepository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithMessage("Workout not found")
            .WithErrorCode(StatusCode.NotFound);
    }
}