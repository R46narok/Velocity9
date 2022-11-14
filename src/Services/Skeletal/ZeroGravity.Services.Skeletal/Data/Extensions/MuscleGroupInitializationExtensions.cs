using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ZeroGravity.Services.Skeletal.Commands;

namespace ZeroGravity.Services.Skeletal.Data.Extensions;

public static class MuscleGroupInitializationExtensions
{
    public static async Task InitializeMuscleGroups(this WebApplication app, IServiceProvider provider)
    {
        var mediator = provider.GetService<IMediator>()!;

        var chestCommand = new CreateMuscleGroupCommand("Chest", "The main function of this chest muscle as a whole is the adduction and internal rotation of the arm on the shoulder joint.");
        
        await mediator.Send(chestCommand);
        
        var backCommand = new CreateMuscleGroupCommand("Back", "Your back muscles are the main structural support for your trunk");
        await mediator.Send(backCommand);
        
        var legsCommand = new CreateMuscleGroupCommand("Legs", "Your leg muscles help you move, carry the weight of your body and support you when you stand.");
        await mediator.Send(legsCommand);
        
        var armsCommand = new CreateMuscleGroupCommand("Arms", "Your arm muscles help you move your arms, hands, fingers and thumbs.");
        await mediator.Send(armsCommand);
        
        var abs = new CreateMuscleGroupCommand("Abs", "The abdominal muscles support the trunk, allow movement and hold organs in place by regulating internal abdominal pressure.");
        await mediator.Send(abs);
    }
}