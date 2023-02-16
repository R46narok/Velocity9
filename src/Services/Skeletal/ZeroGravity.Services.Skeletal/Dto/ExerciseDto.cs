using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Dto;

public class ExerciseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}
