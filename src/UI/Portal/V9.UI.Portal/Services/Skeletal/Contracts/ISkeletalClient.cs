using ErrorOr;
using Refit;
using V9.UI.Portal.Services.Skeletal.Requests;
using V9.UI.Portal.Services.Skeletal.Views;

namespace V9.UI.Portal.Services.Skeletal.Contracts;

public interface ISkeletalClient
{
    [Get("/health")]
    public Task<IApiResponse> HealthAsync();
    
    [Get(Endpoints.ExerciseBase)]
    public Task<IApiResponse<List<ExerciseView>>> GetAllExercisesAsync();

    [Get($"{Endpoints.ExerciseBase}/details")]
    public Task<IApiResponse<ExerciseView>> GetExerciseByNameAsync([Query] string name);


    [Get(Endpoints.MuscleBase)]
    public Task<IApiResponse<List<MuscleView>>> GetAllMusclesAsync();

    [Get($"{Endpoints.MuscleBase}/image")]
    public Task<IApiResponse<byte[]>> GetMuscleImageAsync([Query] string name);

    [Post(Endpoints.ExerciseBase)]
    public Task<IApiResponse> CreateExerciseAsync([Body] CreateExerciseRequest command);
}