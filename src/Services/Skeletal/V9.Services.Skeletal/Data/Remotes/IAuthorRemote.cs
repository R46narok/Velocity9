using Microsoft.AspNetCore.Mvc;
using Refit;
using V9.Application;
using V9.Application.Interfaces;

namespace V9.Services.Skeletal.Data.Remotes;

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