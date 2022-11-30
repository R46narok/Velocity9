using Refit;
using ZeroGravity.UI.Portal.Models;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Exercise.Contracts;

// TODO: Rename
public interface ISkeletalApi
{
    [Get("/api/exercise")]
    public Task<Domain.Types.ApiResponse<List<ExerciseModel>>> GetAllExercises();

    [Get("/api/muscle")]
    public Task<Domain.Types.ApiResponse<List<MuscleModel>>> GetAllMuscles();
}