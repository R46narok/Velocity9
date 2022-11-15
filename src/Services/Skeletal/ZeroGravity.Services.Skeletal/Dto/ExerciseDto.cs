using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Dto;

public class ExerciseDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Muscle> Targets { get; set; }
    public Author Author { get; set; }
}
