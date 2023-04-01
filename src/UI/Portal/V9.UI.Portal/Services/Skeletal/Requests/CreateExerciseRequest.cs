namespace V9.UI.Portal.Services.Skeletal.Requests;

public class CreateExerciseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> TargetNames { get; set; }
    public string AuthorName { get; set; }
}