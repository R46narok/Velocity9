using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Exercises.Data.Entities;

public class Muscle : EntityBase<int>
{
    public string Group { get; set; }
    public string Description { get; set; }
    public int HeadCount { get; set; }
}