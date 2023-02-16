using Refit;
using ZeroGravity.UI.Portal.Services.Authorization.Requests;

namespace ZeroGravity.UI.Portal.Services.Authorization.Contracts;

public interface IAuthorizationClient
{
    [Post(Endpoints.TokenBase)]
    public Task<Domain.Types.PipelineResult<string>> GetTokenAsync(SignInRequest request);
}