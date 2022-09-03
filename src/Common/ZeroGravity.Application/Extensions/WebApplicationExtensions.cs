using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ZeroGravity.Application.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePersistence<T>(this WebApplication app) where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<T>();

        db?.Database.EnsureCreated();
    }
}