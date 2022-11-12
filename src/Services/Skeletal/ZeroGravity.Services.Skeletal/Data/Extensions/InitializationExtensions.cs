using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ZeroGravity.Services.Skeletal.Data.Extensions;

public static class InitializationExtensions
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var provider = scope.ServiceProvider;

        await app.InitializeFibers(provider);
        await app.InitializeMuscleGroups(provider);
        await app.InitializeMuscles(provider);
    }
}