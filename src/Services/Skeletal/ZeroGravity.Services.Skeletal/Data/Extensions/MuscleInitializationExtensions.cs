using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ZeroGravity.Services.Skeletal.Commands;

namespace ZeroGravity.Services.Skeletal.Data.Extensions;

public static class MuscleInitializationExtensions
{
    public static async Task InitializeMuscles(this WebApplication app, IServiceProvider provider)
    {
        var mediator = provider.GetService<IMediator>()!;

        CreateMuscleCommand[] commands = {
            new("Rectus abdominis", "", 1, 1, 1, "Abs"),
            new("External oblique", "", 1, 1, 1, "Abs"),
            new("Internal oblique", "", 1, 1, 1, "Abs"),
            new("Transversus abdominis", "", 1, 1, 1, "Abs"),
            
            new("Latissimus dorsi", "", 1, 1, 1, "Back"),
            new("Trapezius", "", 1, 1, 1, "Back"),
            new("Rhomboid major", "", 1, 1, 1, "Back"),
            new("Rhomboid minor", "", 1, 1, 1, "Back"),
            new("Levator scupulae", "", 1, 1, 1, "Back"),
            
            new("Pectoralis major", "", 1, 1, 1, "Chest"),
            new("Pectoralis minor", "", 1, 1, 1, "Chest"),
            
            new("Deltoid", "", 1, 1, 1, "Arms"),
            new("Biceps", "", 1, 1, 1, "Arms"),
            new("Triceps", "", 1, 1, 1, "Arms"),
        };

        foreach (var createMuscleCommand in commands)
        {
            await mediator.Send(createMuscleCommand);
        }
    }
}