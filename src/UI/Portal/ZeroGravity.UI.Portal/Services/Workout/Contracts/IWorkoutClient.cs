using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Services.Skeletal.Views;
using ZeroGravity.UI.Portal.Services.Workout.Requests;
using ZeroGravity.UI.Portal.Services.Workout.Views;

namespace ZeroGravity.UI.Portal.Services.Workout.Contracts;

public interface IWorkoutClient
{
    [Get("/api/workout")]
    public Task<IApiResponse<CqrsResult<WorkoutView>>> GetWorkoutAsync([Query] GetWorkoutRequest request);
}