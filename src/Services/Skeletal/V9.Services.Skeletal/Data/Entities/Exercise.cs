using V9.Domain.Entities;

namespace V9.Services.Skeletal.Data.Entities;

public class Exercise : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Muscle> Targets { get; set; }
    public Author Author { get; set; }
}