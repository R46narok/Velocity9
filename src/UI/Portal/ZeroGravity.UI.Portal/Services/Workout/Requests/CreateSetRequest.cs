namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class CreateSetRequest
{
    public string Notes { get; set; }
    public int TargetReps { get; set; }
    public int CompletedReps { get; set; }
    public string ExerciseName { get; set; }
    public string WorkoutName { get; set; }
    public string UserName { get; set; }
}

