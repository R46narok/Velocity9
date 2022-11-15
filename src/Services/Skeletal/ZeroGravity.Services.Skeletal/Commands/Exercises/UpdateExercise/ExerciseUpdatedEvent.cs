using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.UpdateExercise;

public class ExerciseUpdatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
}