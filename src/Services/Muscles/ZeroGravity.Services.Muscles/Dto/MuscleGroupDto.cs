using ZeroGravity.Services.Muscles.Data.Entities;

namespace ZeroGravity.Services.Muscles.Dto;

public class MuscleGroupDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Muscle> Muscles { get; set; }}