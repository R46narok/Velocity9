using V9.Domain.Events;

namespace V9.Services.Workout.Commands;

public class WorkoutUpdatedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Notes { get; set; }
    public DateTime CompletedOn { get; set; }
} 
