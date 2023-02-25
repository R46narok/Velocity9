using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Services.Workout.Requests;

namespace ZeroGravity.UI.Portal.Services.Workout.Contracts;

public interface ISetClient
{
    [Patch("/api/set")]
    public Task<IApiResponse<CqrsResult>> UpdateSetAsync(UpdateSetRequest request);
}