using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateSetCommandValidator : AbstractValidator<CreateSetCommand>
{
    public CreateSetCommandValidator(IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository)
    {
        RuleFor(cmd => new {cmd.WorkoutName, cmd.UserName})
            .MustAsync(async (prop, _) =>
                await workoutRepository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is not null)
            .WithName("WorkoutName")
            .WithErrorCode("Workout not present in the database");

        RuleFor(cmd => cmd.ExerciseName)
            .MustAsync(async (name, _) => await exerciseRepository.GetByNameAsync(name, false) is not null)
            .WithErrorCode("Exercise not present in the database");
        
        RuleFor(cmd => cmd.TargetReps)
            .GreaterThan(0);
    }
}