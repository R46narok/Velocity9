using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Workout.Commands;

public class WorkoutDeletedEvent : IDomainEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
}