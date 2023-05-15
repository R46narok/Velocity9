using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FeatureManagement;
using Serilog;
using V9.UI.Core.Providers;
using V9.UI.Portal.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<TokenAuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddDataProtection();

builder.AddApplication();

builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore(opt =>
{
    opt.AddPolicy("Developer", b => b.RequireClaim(ClaimTypes.Role, "Developer", "Admin"));
    opt.AddPolicy("Administrator", b => b.RequireClaim(ClaimTypes.Role, "Admin"));
});

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