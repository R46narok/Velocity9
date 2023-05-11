using AutoMapper.Configuration.Annotations;
using V9.Domain.Entities;

namespace V9.Services.Skeletal.Data.Entities;

public class Muscle : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public float TypeOneFiberPercentage { get; set; }
    public float TypeTwoFiberPercentage { get; set; }
    public float TypeThreeFiberPercentage { get; set; }
    
    public byte[]? Image { get; set; }
    
    [Ignore]
    public MuscleGroup Group { get; set; }
    
    [Ignore]
    public List<Exercise> Exercises { get; set; }
}