namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class CreateWorkoutRequest
{
    public string WorkoutName { get; }
    public string Notes { get; }
    public int Type { get; }
    public string UserName { get; } = "";

    public CreateWorkoutRequest(string workoutName, string notes, int type)
    {
        WorkoutName = workoutName;
        Notes = notes;
        Type = type;
    }
}