using ErrorOr;
using Refit;

namespace ZeroGravity.Application.Interfaces;

public interface IRemoteEntityProvider<T>
{
    public Task<IApiResponse<ErrorOr<List<T>>>> GetAllAsync();
}