using Refit;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Application.Interfaces;

public interface IRemoteEntityProvider<T>
{
    public Task<IApiResponse<PipelineResult<List<T>>>> GetAllAsync();
}