using Refit;
using V9.Application;

namespace V9.Services.Workout.Data.Remotes;

public class RemoteExercise
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> TargetNames { get; set; }
    public string AuthorName { get; set; }
}

public interface IRemoteExerciseProvider : IRemoteSynchronizerProvider<RemoteExercise>
{
    [Get("/api/exercise")]
    Task<IApiResponse<List<RemoteExercise>>> GetAllAsync();
}
