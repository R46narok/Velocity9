using Refit;
using V9.UI.Portal.Services.Preferences.Requests;
using V9.UI.Portal.Services.Preferences.Views;

namespace V9.UI.Portal.Services.Preferences.Contracts;

public interface IPreferencesClient
{
    [Get(Endpoints.PreferencesBase)]
    public Task<IApiResponse<PreferencesView>> GetAsync([Header("Authorization")]  string bearer);
    
    [Post(Endpoints.PreferencesBase)]
    public Task<IApiResponse> CreateAsync(
        [Header("Authorization")] string bearer, 
        [Body] CreatePreferencesRequest request);
}