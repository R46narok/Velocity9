using System.ComponentModel.DataAnnotations;
using V9.Domain.Entities;
using V9.Services.Skeletal.Data.Enums;

#pragma warning disable CS8618

namespace V9.Services.Skeletal.Data.Entities;

public class Exercise : EntityBase<int>
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    
    public ExerciseDifficulty Difficulty { get; set; }
    public string[] ExecutionSteps { get; set; }
    public bool IsWeighted { get; set; }
    public byte[]? Thumbnail { get; set; }
    public byte[]? Video { get; set; }
    
    public List<Muscle> Targets { get; set; }
    public Author Author { get; set; }
}