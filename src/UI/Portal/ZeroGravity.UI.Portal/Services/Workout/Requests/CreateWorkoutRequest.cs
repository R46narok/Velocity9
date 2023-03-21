namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class CreateWorkoutRequest
{
    public string WorkoutName { get; }
    public string Notes { get; }
    public int Type { get; }

    // ReSharper disable once UnusedMember.Global
    public string UserName => "";

    public CreateWorkoutRequest(string workoutName, string notes, int type)
    {
        WorkoutName = workoutName;
        Notes = notes;
        Type = type;
    }
}