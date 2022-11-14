using Refit;
using ZeroGravity.UI.Portal.Blazor.Pages.User.Models;

namespace ZeroGravity.UI.Portal.Blazor.Pages.User.Clients;

public interface IAuthenticationApi
{
    [Post("/api/user")]
    public Task CreateUser(SignUpModel model);
}