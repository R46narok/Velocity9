using V9.Domain.Entities;

namespace V9.Services.Workout.Data.Entities;

public class User : EntityBase<int>
{
    public string ExternalId { get; set; }
    public string UserName { get; set; }
    
    public List<Exercise> Authored { get; set; }
    public List<Workout> Workouts { get; set; }
    public Preferences Preferences { get; set; }
}