namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class GetWorkoutRequest
{
    public GetWorkoutRequest(string userName, string workoutName)
    {
        UserName = userName;
        WorkoutName = workoutName;
    }

    public string UserName { get; set; }
    public string WorkoutName { get; set; }
}