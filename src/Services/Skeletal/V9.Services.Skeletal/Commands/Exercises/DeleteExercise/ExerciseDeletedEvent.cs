using System.Runtime.InteropServices.ComTypes;
using V9.Domain.Events;

namespace V9.Services.Skeletal.Commands.Exercises.DeleteExercise;

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