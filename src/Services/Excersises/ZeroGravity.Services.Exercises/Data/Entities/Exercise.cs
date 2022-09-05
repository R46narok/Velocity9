using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Exercises.Data.Entities;

public class Exercise : EntityBase<int>
{
   public string Name { get; set; }
   
   public Author Author { get; set; }
   public List<Muscle> TargetMuscles { get; set; }
}