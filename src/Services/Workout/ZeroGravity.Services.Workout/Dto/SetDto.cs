namespace ZeroGravity.Services.Workout.Dto;

public class SetDto
{
     public string? Notes { get; set; }
     
     public int TargetReps { get; set; }
     public int CompletedReps { get; set; }
     
     public string ExerciseName { get; set; }
}