namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class GetWorkoutRequest
{
    public GetWorkoutRequest(string workoutName)
    {
        WorkoutName = workoutName;
    }

    public string WorkoutName { get; set; }
}