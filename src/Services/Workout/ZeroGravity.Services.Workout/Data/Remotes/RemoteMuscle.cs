using Refit;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Services.Workout.Data.Remotes;

public class RemoteMuscle
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IRemoteMuscleProvider : IRemoteSynchronizerProvider<RemoteMuscle>
{
    [Get("/api/muscle")]
    Task<IApiResponse<PipelineResult<List<RemoteMuscle>>>> GetAllAsync();
}
