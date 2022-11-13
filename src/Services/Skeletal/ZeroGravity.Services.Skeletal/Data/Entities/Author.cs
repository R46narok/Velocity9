using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Skeletal.Data.Entities;

public class Author : EntityBase<int>
{
    public string ExternalId { get; set; }
    public string UserName { get; set; }
    public List<Exercise> Exercises { get; set; }
}