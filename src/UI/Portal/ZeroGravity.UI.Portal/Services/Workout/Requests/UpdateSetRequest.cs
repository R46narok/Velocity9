namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class UpdateSetRequest
{
    public UpdateSetRequest(string? notes, int? completedReps, int index, string workoutName, string userName)
    {
        this.Notes = notes;
        this.CompletedReps = completedReps;
        this.Index = index;
        this.WorkoutName = workoutName;
        this.UserName = userName;
    }

    public string? Notes { get; init; }
    public int? CompletedReps { get; init; }
    public int Index { get; init; }
    public string WorkoutName { get; init; }
    public string UserName { get; init; }
}