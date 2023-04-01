using V9.Domain.Events;

namespace V9.Services.Workout.Commands;

public class WorkoutDeletedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
}