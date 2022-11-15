using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Blazor.Models;

namespace ZeroGravity.UI.Portal.Blazor.Contracts;

public interface ISkeletalApi
{
    [Get("/api/exercise")]
    public Task<Domain.Types.ApiResponse<List<ExerciseModel>>> GetAllExercises();
}