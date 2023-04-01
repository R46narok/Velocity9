using V9.Domain.Events;
using V9.Services.Workout.Data.Entities;

namespace V9.Services.Workout.Commands;

public class WorkoutCreatedEvent : IDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }
}