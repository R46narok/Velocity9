using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Coach.Data.Entities;

public class Exercise : EntityBase<int>
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
}