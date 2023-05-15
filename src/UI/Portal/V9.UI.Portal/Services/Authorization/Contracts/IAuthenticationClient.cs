using Microsoft.AspNetCore.Mvc;
using Refit;
using V9.UI.Portal.Services.Authorization.Requests;
using V9.UI.Portal.Services.Authorization.Views;

namespace V9.UI.Portal.Services.Authorization.Contracts;

public interface IAuthenticationClient
{
    [Get("/health")]
    public Task<IApiResponse> HealthAsync();
    
    
    [Get("/api/user/all")]
    public Task<IApiResponse<List<UserView>>> GetAllAsync();

    [Post(Endpoints.UserBase)]
    public Task<IApiResponse> CreateUserAsync(SignUpRequest request);
    
    [Delete(Endpoints.UserBase)]
    public Task<IApiResponse> DeleteUserAsync([Query] string name);
    
    [Get(Endpoints.UserBase)]
    public Task<IApiResponse<UserView>> GetUserAsync([Query] string userName);

    
    [Put(Endpoints.UserBase)]
    public Task<IApiResponse> ElevateUserAsync([Query] string username, [Query] string role, [Query] string? id);
}