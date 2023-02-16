using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Dto;

public class MuscleDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    
    public float TypeOneFiberPercentage { get; set; }
    public float TypeTwoFiberPercentage { get; set; } 
    public float TypeThreeFiberPercentage { get; set; }
    
}