using V9.Domain.Entities;

namespace V9.Services.Skeletal.Data.Entities;

public class MuscleGroup : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Muscle> Muscles { get; set; }
}