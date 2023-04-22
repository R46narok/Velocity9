using MediatR;
using V9.UI.Portal.Services.Skeletal.Enums;

namespace V9.UI.Portal.Blazor.Pages.Exercise.Explore.ViewModels;

public class CreateExerciseViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsWeighted { get; set; }
    public int ExerciseDifficulty { get; set; }
    public string ExecutionSteps { get; set; }

    public byte[]? Thumbnail { get; set; }
    public byte[]? Video { get; set; }
    
    public List<string> TargetNames { get; set; } = new();
    public string CurrentTarget { get; set; }
    public string AuthorName { get; set; }
}
