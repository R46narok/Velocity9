using V9.Domain.Events;
using V9.Services.Skeletal.Data.Entities;

namespace V9.Services.Skeletal.Commands.Exercises.CreateExercise;

public class ExerciseCreatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}