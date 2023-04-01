using V9.Services.Skeletal.Data.Entities;

namespace V9.Services.Skeletal.Dto;

public class MuscleGroupDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Muscle> Muscles { get; set; }}