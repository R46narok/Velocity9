using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Authorization.Data.Entities;
using ZeroGravity.Services.Authorization.Data.Persistence;

var factory = new ConnectionFactory() {HostName = "localhost"};

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
// Data layer
builder.AddPersistence<UserDbContext>();

// Mediator 
builder.Services.AddMediatorAndVluentValidation(new[] {typeof(User).Assembly});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();
builder.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePersistence<UserDbContext>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
