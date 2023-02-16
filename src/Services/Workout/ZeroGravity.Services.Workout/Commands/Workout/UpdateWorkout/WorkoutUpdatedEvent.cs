using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Workout.Commands;

public class WorkoutUpdatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Notes { get; set; }
    public DateTime CompletedOn { get; set; }
} 
