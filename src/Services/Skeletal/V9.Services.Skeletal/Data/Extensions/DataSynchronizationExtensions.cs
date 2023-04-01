using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using V9.Application;
using V9.Application.Extensions;
using V9.Services.Skeletal.Commands.CreateAuthor;
using V9.Services.Skeletal.Data.Remotes;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Persistence;

namespace V9.Services.Skeletal.Data.Extensions;

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