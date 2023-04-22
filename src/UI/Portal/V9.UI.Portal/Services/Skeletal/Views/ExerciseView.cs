using System.Reflection;
using V9.UI.Portal.Services.Skeletal.Enums;

namespace V9.UI.Portal.Services.Skeletal.Views;

public class ExerciseView
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }

    public ExerciseDifficulty Difficulty { get; set; }
    public string[] ExecutionSteps { get; set; }
    public bool IsWeighted { get; set; }
    
    public byte[]? Thumbnail { get;set; }
    public byte[]? Video { get;set; }

    public List<string> TargetNames { get; set; }
    public string AuthorName { get; set; }
}