using ErrorOr;
using Refit;
using ZeroGravity.UI.Portal.Services.Authorization.Requests;

namespace ZeroGravity.UI.Portal.Services.Authorization.Contracts;

public class GetTokenResponse
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
}

public interface IAuthorizationClient
{
    [Post(Endpoints.TokenBase)]
    public Task<IApiResponse<GetTokenResponse>> GetTokenAsync(SignInRequest request);
}