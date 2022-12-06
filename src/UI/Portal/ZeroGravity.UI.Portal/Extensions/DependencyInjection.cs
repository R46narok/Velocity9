using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ZeroGravity.UI.Portal.Services.Authorization.Contracts;
using ZeroGravity.UI.Portal.Services.Skeletal.Contracts;

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
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(config["Services:Skeletal:Rest"]));
    }
}