using Refit;
using ZeroGravity.UI.Portal.Blazor.Pages.Profile.Models;
using ZeroGravity.UI.Portal.Models;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Profile.Contracts;

public interface IAuthorizationApi
{
    [Post("/api/token")]
    public Task<Domain.Types.ApiResponse<string>> GetToken(SignInModel model);
}