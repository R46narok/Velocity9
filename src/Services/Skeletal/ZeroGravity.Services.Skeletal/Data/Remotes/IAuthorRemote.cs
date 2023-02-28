using Microsoft.AspNetCore.Mvc;
using Refit;
using ZeroGravity.Application;
using ZeroGravity.Application.Interfaces;

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
    Task<IApiResponse<List<RemoteUser>>> GetAllAsync();
}