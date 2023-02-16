using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ZeroGravity.Application;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Remotes;

namespace ZeroGravity.Services.Workout.Data.Extensions;

public static class DataSynchronizationExtensions
{
    public static async Task SynchronizeDataFromRemotes(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var config = app.Configuration;
        var provider = scope.ServiceProvider;
        
        var mapper = provider.GetService<IMapper>()!;
        var mediator = provider.GetService<IMediator>()!;

        var skeletalRemote = RestService.For<IRemoteMuscleProvider>(config["Services:Skeletal:Rest"]);
        var exerciseRemote = RestService.For<IRemoteExerciseProvider>(config["Services:Skeletal:Rest"]);
        var userRemote = RestService.For<IRemoteUserProvider>(config["Services:Authorization:Rest"]);

        var userSync = new DataSynchronizer<RemoteUser, CreateUserCommand>(mapper, mediator, userRemote);
        await userSync.Synchronize();
        var sync = new DataSynchronizer<RemoteMuscle, CreateMuscleCommand>(mapper, mediator, skeletalRemote);
        await sync.Synchronize();

        var exerciseSync =
            new DataSynchronizer<RemoteExercise, CreateExerciseCommand>(mapper, mediator, exerciseRemote);
        await exerciseSync.Synchronize();

    }
}
