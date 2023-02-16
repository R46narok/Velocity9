using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Workout.Data.Entities;

namespace ZeroGravity.Services.Workout.Commands;

public class WorkoutCreatedEvent : IDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }
}