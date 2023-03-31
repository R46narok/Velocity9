using Refit;
using ZeroGravity.UI.Portal.Services.Preferences.Requests;
using ZeroGravity.UI.Portal.Services.Preferences.Views;

namespace ZeroGravity.UI.Portal.Services.Preferences.Contracts;

public interface IPreferencesClient
{
    [Get(Endpoints.PreferencesBase)]
    public Task<IApiResponse<PreferencesView>> GetAsync([Header("Authorization")]  string bearer);
    
    [Post(Endpoints.PreferencesBase)]
    public Task<IApiResponse> CreateAsync(
        [Header("Authorization")] string bearer, 
        [Body] CreatePreferencesRequest request);
}