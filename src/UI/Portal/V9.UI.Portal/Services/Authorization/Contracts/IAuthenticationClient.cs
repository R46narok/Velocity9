using Microsoft.AspNetCore.Mvc;
using Refit;
using V9.UI.Portal.Services.Authorization.Requests;
using V9.UI.Portal.Services.Authorization.Views;

namespace V9.UI.Portal.Services.Authorization.Contracts;

public interface IAuthenticationClient
{
    [Post(Endpoints.UserBase)]
    public Task<IApiResponse> CreateUserAsync(SignUpRequest request);
    
    [Get(Endpoints.UserBase)]
    public Task<IApiResponse<UserView>> GetUserAsync([Query] string userName);

}