using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Services.Authorization.Requests;

namespace ZeroGravity.UI.Portal.Services.Authorization.Contracts;

public interface IAuthenticationClient
{
    [Post(Endpoints.UserBase)]
    public Task<IApiResponse<PipelineResult>> CreateUserAsync(SignUpRequest request);
}