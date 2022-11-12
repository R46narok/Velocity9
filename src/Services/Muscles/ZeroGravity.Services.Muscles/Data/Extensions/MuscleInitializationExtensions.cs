using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ZeroGravity.Services.Muscles.Commands;

namespace ZeroGravity.Services.Muscles.Data.Extensions;

public static class MuscleInitializationExtensions
{
    public static async Task InitializeMuscles(this WebApplication app, IServiceProvider provider)
    {
        var mediator = provider.GetService<IMediator>()!;

        var command = new CreateMuscleCommand("Latissimus Dorsi", "Desc",
            1.0f, 0.0f, 0.0f, "Back");
        await mediator.Send(command);
    }
}