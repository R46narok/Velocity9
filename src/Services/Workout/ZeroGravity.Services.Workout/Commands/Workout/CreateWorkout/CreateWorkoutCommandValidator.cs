using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
{
    public CreateWorkoutCommandValidator(IWorkoutRepository workoutRepository, IUserRepository userRepository)
    {
        RuleFor(cmd => new {cmd.WorkoutName, cmd.UserName})
            .MustAsync(async (prop, _) => await workoutRepository.GetByNameAsync(prop.UserName, prop.WorkoutName, false) is null)
            .WithName("Workout")
            .WithErrorCode("Workout already present in the database");

        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (name, _) => await userRepository.GetByNameAsync(name, false) is not null)
            .WithErrorCode("User does not exist in the database");

        RuleFor(cmd => cmd.WorkoutName)
            .NotEmpty();
    }
}