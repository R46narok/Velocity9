using Refit;
using ZeroGravity.Application;

namespace ZeroGravity.Services.Workout.Data.Remotes;

public class RemoteMuscle
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IRemoteMuscleProvider : IRemoteSynchronizerProvider<RemoteMuscle>
{
    [Get("/api/muscle")]
    Task<IApiResponse<List<RemoteMuscle>>> GetAllAsync();
}
