using Refit;
using ZeroGravity.UI.Portal.Blazor.Pages.Profile.Models;
using ZeroGravity.UI.Portal.Models;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Profile.Contracts;

public interface IAuthenticationApi
{
    [Post("/api/user")]
    public Task CreateUser(SignUpModel model);
}