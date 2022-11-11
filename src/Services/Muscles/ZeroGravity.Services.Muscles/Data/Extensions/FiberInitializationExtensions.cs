using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ZeroGravity.Services.Muscles.Commands;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Data.Extensions;

public static class FiberInitializationExtensions 
{
    public static async Task InitializeFibers(this WebApplication app, IServiceProvider provider)
    {
        var mediator = provider.GetService<IMediator>()!;
        var repository = provider.GetService<IFiberRepository>()!;

        if (await repository.GetByNameAsync("Type One") is null)
        {
            var cmd = new CreateFiberCommand(
                "Type One",
                "Sample description",
                MotorUnitType.SlowOxidative, TwitchSpeed.Slow, TwitchForce.Small,
                ResistanceToFatigue.High);
            await mediator.Send(cmd);
        }
        
        if (await repository.GetByNameAsync("Type Two A") is null)
        {
            var cmd = new CreateFiberCommand(
                "Type Two A",
                "Sample description",
                MotorUnitType.FastOxidative, TwitchSpeed.Fast, TwitchForce.Medium,
                ResistanceToFatigue.High);
            await mediator.Send(cmd);
        }
        
        if (await repository.GetByNameAsync("Type Two X") is null)
        {
            var cmd = new CreateFiberCommand(
                "Type Two X",
                "Sample description",
                MotorUnitType.FastGlycolytic, TwitchSpeed.Fast, TwitchForce.Large,
                ResistanceToFatigue.Low);
            await mediator.Send(cmd);
        }
    }
    
}