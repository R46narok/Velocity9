using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ZeroGravity.UI.Core.Providers;
using ZeroGravity.UI.Portal.Security;
using ZeroGravity.UI.Portal.Services.Authorization.Contracts;
using ZeroGravity.UI.Portal.Services.Coach.Contracts;
using ZeroGravity.UI.Portal.Services.Skeletal.Contracts;
using ZeroGravity.UI.Portal.Services.Workout.Contracts;

namespace ZeroGravity.UI.Portal.Extensions;

public static class DependencyInjection
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var services = builder.Services;

        builder.Services
            .AddRefitClient<IAuthenticationClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(config["Services:Authorization:Rest"]));

        builder.Services
            .AddRefitClient<IAuthorizationClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(config["Services:Authorization:Rest"]));

        builder.Services
            .AddRefitClient<ISkeletalClient>()
            .ConfigureHttpClient((sp, x) => x.BaseAddress = new Uri(config["Services:Skeletal:Rest"]));


        builder.Services.AddScoped<AuthorizationHttpClientHandler>();
        builder.Services
            .AddRefitClient<IWorkoutClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(config["Services:Workout:Rest"]))
            .AddHttpMessageHandler<AuthorizationHttpClientHandler>();

        builder.Services
            .AddRefitClient<ISetClient>()
            .SetHandlerLifetime(TimeSpan.FromSeconds(1))
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(config["Services:Workout:Rest"]));
        
        builder.Services
            .AddRefitClient<IAnomalyClient>()
            .ConfigureHttpClient(x =>
            {
                x.BaseAddress = new Uri(config["Services:Coach:Rest"]);
                x.Timeout = TimeSpan.FromSeconds(30);
            });
    }
}