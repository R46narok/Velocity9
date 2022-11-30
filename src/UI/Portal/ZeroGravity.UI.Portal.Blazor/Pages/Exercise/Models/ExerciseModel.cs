namespace ZeroGravity.UI.Portal.Models;

public class ExerciseModel
{
     public string Name { get; set; }
     public string Description { get; set; }
   
     public List<MuscleModel> Targets { get; set; }
     public AuthorModel Author { get; set; }
}