using V9.Domain.Entities;

namespace V9.Services.Workout.Data.Entities;

public class Preferences : EntityBase<int>
{
    public double ExerciseRestTime { get; set; }
    public double SetRestTime { get; set; }
    
    public User User { get; set; }
}