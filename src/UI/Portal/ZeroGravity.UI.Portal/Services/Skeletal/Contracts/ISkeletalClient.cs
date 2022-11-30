using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Services.Skeletal.Views;

namespace ZeroGravity.UI.Portal.Services.Skeletal.Contracts;

// TODO: Rename
public interface ISkeletalClient
{
    [Get(Endpoints.ExerciseBase)]
    public Task<IApiResponse<PipelineResult<List<ExerciseView>>>> GetAllExercisesAsync();

    [Get(Endpoints.MuscleBase)]
    public Task<IApiResponse<PipelineResult<List<MuscleView>>>> GetAllMusclesAsync();
}