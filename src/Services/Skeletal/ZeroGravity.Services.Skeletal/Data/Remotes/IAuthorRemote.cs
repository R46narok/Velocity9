using Microsoft.AspNetCore.Mvc;
using Refit;
using ZeroGravity.Application;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Services.Skeletal.Data.Remotes;

public class RemoteUser
{
     public string Id { get; set; }
     public string UserName { get; set; }
     public string Email { get; set; }
     public string Phone { get; set; }
}

public interface IAuthorRemote : IRemoteSynchronizerProvider<RemoteUser>
{
    [Get("/api/user/all")]
    Task<IApiResponse<CqrsResult<List<RemoteUser>>>> GetAllAsync();
}