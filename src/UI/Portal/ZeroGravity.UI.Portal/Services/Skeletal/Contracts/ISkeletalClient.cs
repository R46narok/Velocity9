using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Services.Skeletal.Requests;
using ZeroGravity.UI.Portal.Services.Skeletal.Views;

namespace ZeroGravity.UI.Portal.Services.Skeletal.Contracts;

// TODO: Rename
public interface ISkeletalClient
{
    [Get(Endpoints.ExerciseBase)]
    public Task<IApiResponse<CqrsResult<List<ExerciseView>>>> GetAllExercisesAsync();

    [Get(Endpoints.MuscleBase)]
    public Task<IApiResponse<CqrsResult<List<MuscleView>>>> GetAllMusclesAsync();

    [Post(Endpoints.ExerciseBase)]
    public Task<IApiResponse<CqrsResult>> CreateExerciseAsync(CreateExerciseRequest request);
}