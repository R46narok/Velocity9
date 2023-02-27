using Refit;
using ZeroGravity.Application;

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
    Task<IApiResponse<List<RemoteUser>>> GetAllAsync();
}
