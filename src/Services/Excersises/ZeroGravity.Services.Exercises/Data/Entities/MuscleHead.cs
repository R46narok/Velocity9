using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Exercises.Data.Entities;

public class MuscleHead : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public MuscleGroup Group { get; set; }

    public MuscleHead(string name, string description, MuscleGroup group)
    {
        Name = name;
        Description = description;
        Group = group;
    }

    private MuscleHead()
    {
        
    }
}