using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Exercises.Data.Entities;

public class MuscleGroup : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<MuscleHead> Heads { get; set; }

    public MuscleGroup(string name, string description, List<MuscleHead> heads)
    {
        Name = name;
        Description = description;
        Heads = heads;
    }

    private MuscleGroup()
    {
        
    }
}