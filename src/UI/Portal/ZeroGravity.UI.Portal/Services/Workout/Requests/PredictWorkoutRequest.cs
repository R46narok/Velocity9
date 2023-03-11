namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class PredictWorkoutRequest
{
    public string UserName { get; }

    public PredictWorkoutRequest(string userName)
    {
        UserName = userName;
    }
}