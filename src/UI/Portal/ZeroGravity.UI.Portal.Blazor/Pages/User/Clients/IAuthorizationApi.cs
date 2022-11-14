using Refit;
using ZeroGravity.Domain.Types;
using ZeroGravity.UI.Portal.Blazor.Pages.User.Models;

namespace ZeroGravity.UI.Portal.Blazor.Pages.User.Clients;

public interface IAuthorizationApi
{
    [Post("/api/token")]
    public Task<Domain.Types.ApiResponse<string>> GetToken(SignInModel model);
}