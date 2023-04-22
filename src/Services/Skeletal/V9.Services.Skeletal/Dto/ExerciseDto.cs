using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Enums;

namespace V9.Services.Skeletal.Dto;

public class ExerciseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }

    public ExerciseDifficulty Difficulty { get; set; }
    public string[] ExecutionSteps { get; set; }
    public bool IsWeighted { get; set; }
    
    public byte[]? Thumbnail { get; set; }
    public byte[]? Video { get; set; }

    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}