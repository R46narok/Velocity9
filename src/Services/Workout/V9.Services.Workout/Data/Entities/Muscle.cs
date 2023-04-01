using V9.Domain.Entities;

namespace V9.Services.Workout.Data.Entities;

public class Muscle : EntityBase<int>
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
}