using ErrorOr;
using Refit;
using V9.UI.Portal.Services.Skeletal.Requests;
using V9.UI.Portal.Services.Skeletal.Views;

namespace V9.UI.Portal.Services.Skeletal.Contracts;

public interface ISkeletalClient
{
    [Get(Endpoints.ExerciseBase)]
    public Task<IApiResponse<List<ExerciseView>>> GetAllExercisesAsync();

    [Get($"{Endpoints.ExerciseBase}/details")]
    public Task<IApiResponse<ExerciseView>> GetExerciseByNameAsync([Query] string name);
    

    [Get(Endpoints.MuscleBase)]
    public Task<IApiResponse<List<MuscleView>>> GetAllMusclesAsync();

    [Post(Endpoints.ExerciseBase)]
    public Task<IApiResponse> CreateExerciseAsync([Body] CreateExerciseRequest command);
}