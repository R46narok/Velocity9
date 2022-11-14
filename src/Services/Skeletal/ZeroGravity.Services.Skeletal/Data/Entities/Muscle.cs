using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Skeletal.Data.Entities;

public class Muscle : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public float TypeOneFiberPercentage { get; set; }
    public float TypeTwoFiberPercentage { get; set; }
    public float TypeThreeFiberPercentage { get; set; }
    
    public MuscleGroup Group { get; set; }
    public List<Exercise> Exercises { get; set; }
}