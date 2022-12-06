using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Refit;
using Serilog;
using ZeroGravity.UI.Core.Providers;
using ZeroGravity.UI.Portal.Blazor.Pages.Exercise.Contracts;
using ZeroGravity.UI.Portal.Blazor.Pages.Profile.Contracts;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<TokenAuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();

builder.Services.AddAuthenticationCore();

builder.Services.AddRefitClient<IAuthenticationApi>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("http://localhost:5000"));

builder.Services.AddRefitClient<IAuthorizationApi>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("http://localhost:5000"));

builder.Services.AddRefitClient<ISkeletalApi>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("http://localhost:5001"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();