using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Exercises.Data.Entities;

public class Author : EntityBase<int>
{
    public string ExternalId { get; set; }
    public string UserName { get; set; }
}