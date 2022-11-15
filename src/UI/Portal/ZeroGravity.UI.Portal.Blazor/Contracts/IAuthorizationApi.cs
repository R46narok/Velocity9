using Refit;
using ZeroGravity.UI.Portal.Blazor.Models;

namespace ZeroGravity.UI.Portal.Blazor.Contracts;

public interface IAuthorizationApi
{
    [Post("/api/token")]
    public Task<Domain.Types.ApiResponse<string>> GetToken(SignInModel model);
}