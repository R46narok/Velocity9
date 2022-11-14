using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Skeletal.Data.Entities;

public class MuscleGroup : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Muscle> Muscles { get; set; }
}