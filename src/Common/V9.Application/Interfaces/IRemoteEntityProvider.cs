using ErrorOr;
using Refit;

namespace V9.Application.Interfaces;

public interface IRemoteEntityProvider<T>
{
    public Task<IApiResponse<ErrorOr<List<T>>>> GetAllAsync();
}