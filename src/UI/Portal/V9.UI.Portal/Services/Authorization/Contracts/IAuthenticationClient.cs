using Refit;
using V9.UI.Portal.Services.Authorization.Requests;

namespace V9.UI.Portal.Services.Authorization.Contracts;

public interface IAuthenticationClient
{
    [Post(Endpoints.UserBase)]
    public Task<IApiResponse> CreateUserAsync(SignUpRequest request);
}