using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;

public class ExerciseCreatedEvent : IDomainEvent
{
     public string Name { get; set; }
     public string Description { get; set; }
     
     public List<Muscle> Targets { get; set; }
     public Author Author { get; set; }   
}