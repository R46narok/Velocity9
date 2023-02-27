using ErrorOr;
using Refit;
using ZeroGravity.UI.Portal.Services.Workout.Requests;
using ZeroGravity.UI.Portal.Services.Workout.Views;

namespace ZeroGravity.UI.Portal.Services.Workout.Contracts;

public interface IWorkoutClient
{
    [Get("/api/workout")]
    public Task<IApiResponse<WorkoutView>> GetWorkoutAsync([Query] GetWorkoutRequest request);
}