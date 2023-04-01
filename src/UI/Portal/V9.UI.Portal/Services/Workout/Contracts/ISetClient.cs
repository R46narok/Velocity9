using Refit;
using V9.UI.Portal.Services.Workout.Requests;

namespace V9.UI.Portal.Services.Workout.Contracts;

public interface ISetClient
{
    [Patch("/api/set")]
    public Task<IApiResponse> UpdateSetAsync(UpdateSetRequest request);
    
    [Post("/api/set")]
    public Task<IApiResponse> CreateSetAsync(CreateSetRequest request);
}
