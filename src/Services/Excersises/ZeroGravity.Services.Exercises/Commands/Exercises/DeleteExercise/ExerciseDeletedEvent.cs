using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Exercises.Commands.Exercises.DeleteExercise;

public class ExerciseDeletedEvent : IDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
}