using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ZeroGravity.Application;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Services.Skeletal.Commands.CreateAuthor;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Persistence;
using ZeroGravity.Services.Skeletal.Data.Remotes;

namespace ZeroGravity.Services.Skeletal.Data.Extensions;

public static class DataSynchronizationExtensions
{
    public static async Task SynchronizeDataFromRemotes(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var config = app.Configuration;
        var provider = scope.ServiceProvider;
        
        var mapper = provider.GetService<IMapper>()!;
        var mediator = provider.GetService<IMediator>()!;

        var remote = RestService.For<IAuthorRemote>(config["Services:Authorization:Rest"]);

        var sync = new DataSynchronizer<RemoteUser, CreateAuthorCommand>(mapper, mediator, remote);
        await sync.Synchronize();
    }
}