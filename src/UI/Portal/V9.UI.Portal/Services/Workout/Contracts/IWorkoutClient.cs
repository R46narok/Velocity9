using ErrorOr;
using Refit;
using V9.UI.Portal.Services.Workout.Requests;
using V9.UI.Portal.Services.Workout.Views;

namespace V9.UI.Portal.Services.Workout.Contracts;

public interface IWorkoutClient
{
    [Get("/api/workout")]
    public Task<IApiResponse<WorkoutView>> GetWorkoutAsync(
        [Query] GetWorkoutRequest request,
        [Header("Authorization")] string bearer);

    [Get("/api/workout/all")]
    public Task<IApiResponse<List<WorkoutView>>> GetAllWorkoutsAsync(
        [Header("Authorization")] string bearer);

    [Put("/api/workout")]
    public Task<IApiResponse<PredictWorkoutView>> PredictWorkoutAsync(
        [Header("Authorization")] string bearer);

    [Post("/api/workout")]
    public Task<IApiResponse> CreateWorkoutAsync(
        [Body] CreateWorkoutRequest request,
        [Header("Authorization")] string bearer);
}