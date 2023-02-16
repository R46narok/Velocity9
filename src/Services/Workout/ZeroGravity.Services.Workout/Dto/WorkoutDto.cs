using ZeroGravity.Services.Workout.Data.Entities;

namespace ZeroGravity.Services.Workout.Dto;

public class WorkoutDto
{
    public string Name { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }

    public DateTime CompletedOn { get; set; }
    public string UserName { get; set; }
    public List<Set> Sets { get; set; }
}