using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Exercises.Commands.Muscles.CreateMuscle;

public class MuscleCreatedEvent : IDomainEvent
{
    public string Group { get; set; }
    public int HeadCount { get; set; }
}