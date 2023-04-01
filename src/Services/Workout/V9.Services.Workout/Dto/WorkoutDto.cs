using V9.Services.Workout.Data.Entities;

namespace V9.Services.Workout.Dto;

public class WorkoutDto
{
    public string Name { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }

    public DateTime CompletedOn { get; set; }
    public string UserName { get; set; }
    public List<SetDto> Sets { get; set; }
}