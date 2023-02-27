using Refit;
using ZeroGravity.UI.Portal.Services.Authorization.Requests;

namespace ZeroGravity.UI.Portal.Services.Authorization.Contracts;

public interface IAuthenticationClient
{
    [Post(Endpoints.UserBase)]
    public Task<IApiResponse> CreateUserAsync(SignUpRequest request);
}