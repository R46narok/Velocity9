using System.Runtime.InteropServices.ComTypes;
using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.DeleteExercise;

public class ExerciseDeletedEvent : IDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ExerciseDeletedEvent(int id, string name)
    {
        Id = id;
        Name = name;
    }
}