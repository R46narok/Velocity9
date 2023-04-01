using V9.Domain.Events;
using V9.Services.Skeletal.Data.Entities;

namespace V9.Services.Skeletal.Commands.Exercises.UpdateExercise;

public class ExerciseUpdatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
}