using Refit;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Services.Workout.Data.Remotes;

public class RemoteUser
{
     public string Id { get; set; }
     public string UserName { get; set; }
     public string Email { get; set; }
     public string Phone { get; set; }
}

public interface IRemoteUserProvider : IRemoteSynchronizerProvider<RemoteUser>
{
    [Get("/api/user/all")]
    Task<IApiResponse<CqrsResult<List<RemoteUser>>>> GetAllAsync();
}
