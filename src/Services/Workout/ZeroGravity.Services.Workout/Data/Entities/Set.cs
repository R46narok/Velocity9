using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Workout.Data.Entities;

public class Set : EntityBase<int>
{
    public string Notes { get; set; }
    
    public int TargetReps { get; set; }
    public int CompletedReps { get; set; }
    
    public Exercise Exercise { get; set; }
    public Workout Workout { get; set; }
}