using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using V9.Services.Skeletal.Commands;

namespace V9.Services.Skeletal.Data.Extensions;

public static class MuscleInitializationExtensions
{
    public static async Task InitializeMuscles(this WebApplication app, IServiceProvider provider)
    {
        var mediator = provider.GetService<IMediator>()!;

        CreateMuscleCommand[] commands = {
            new("Rectus abdominis", "", 1, 1, 1, "Abs", ReadBytes("Assets/rectus abdominis.png")),
            new("External oblique", "", 1, 1, 1, "Abs", ReadBytes("Assets/external oblique.png")),
            new("Internal oblique", "", 1, 1, 1, "Abs", ReadBytes("Assets/internal oblique.png")),
            // new("Transversus abdominis", "", 1, 1, 1, "Abs", ReadBytes("")),
            
            new("Latissimus dorsi", "", 1, 1, 1, "Back", ReadBytes("Assets/latissimus dorsi.png")),
            new("Trapezius", "", 1, 1, 1, "Back", ReadBytes("Assets/trapezius.png")),
            new("Rhomboid major", "", 1, 1, 1, "Back", ReadBytes("Assets/rhomboid major.png")),
            new("Rhomboid minor", "", 1, 1, 1, "Back", ReadBytes("Assets/rhomboid minor.png")),
            // new("Levator scupulae", "", 1, 1, 1, "Back", ReadBytes("")),
            
            new("Pectoralis major", "", 1, 1, 1, "Chest", ReadBytes("Assets/pectoralis major.png")),
            new("Pectoralis minor", "", 1, 1, 1, "Chest", ReadBytes("Assets/pectoralis minor.png")),
            
            new("Deltoid", "", 1, 1, 1, "Arms", ReadBytes("Assets/deltoid.png")),
            new("Biceps", "", 1, 1, 1, "Arms", ReadBytes("Assets/biceps.png")),
            new("Triceps", "", 1, 1, 1, "Arms", ReadBytes("Assets/triceps.png")),
        };

        foreach (var createMuscleCommand in commands)
        {
            await mediator.Send(createMuscleCommand);
        }
    }

    private static byte[] ReadBytes(string filename)
    {
        return File.ReadAllBytes(filename);
    }
}