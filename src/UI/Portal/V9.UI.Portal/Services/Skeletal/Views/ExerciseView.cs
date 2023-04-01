namespace V9.UI.Portal.Services.Skeletal.Views;

public class ExerciseView
{
     public string Name { get; set; }
     public string Description { get; set; }
   
     public List<MuscleView> Targets { get; set; }
     public AuthorView Author { get; set; }
}