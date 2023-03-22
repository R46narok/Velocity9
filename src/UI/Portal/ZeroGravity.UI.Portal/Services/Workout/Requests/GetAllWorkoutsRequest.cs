namespace ZeroGravity.UI.Portal.Services.Workout.Requests;

public class GetAllWorkoutsRequest
{
    public GetAllWorkoutsRequest(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}
