using FluentValidation;
using V9.Application;
using V9.Services.Workout.Data.Repositories;
using V9.Services.Workout.Queires;

namespace V9.Services.Workout.Queries;

public class GetWorkoutQueryValidator : AbstractValidator<GetWorkoutQuery>
{
    public GetWorkoutQueryValidator(IWorkoutRepository workoutRepository)
    {
        RuleFor(query => new {query.UserName, query.WorkoutName})
            .MustAsync(async (prop, _) =>
                await workoutRepository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithName("Workout")
            .WithErrorCode("Workout not found");
    }
}