using Refit;
using ZeroGravity.UI.Portal.Blazor.Models;

namespace ZeroGravity.UI.Portal.Blazor.Contracts;

public interface IAuthenticationApi
{
    [Post("/api/user")]
    public Task CreateUser(SignUpModel model);
}