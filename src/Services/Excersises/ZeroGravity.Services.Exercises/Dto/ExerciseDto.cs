using ZeroGravity.Services.Exercises.Data.Entities;

namespace ZeroGravity.Services.Exercises.Dto;

public class ExerciseDto
{
    public string Name { get; set; }
    public Author Author { get; set; }
    public List<Muscle> TargetMuscles { get; set; }
}