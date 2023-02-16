using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Workout.Data.Entities;

#nullable disable

public class Exercise : EntityBase<int>
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    
    public User Author { get; set; }
    public List<Muscle> Targets { get; set; }
    public List<Exercise> Sets { get; set; }
}