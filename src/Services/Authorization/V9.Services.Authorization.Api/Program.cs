using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using V9.Application.Extensions;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Infrastructure.MessageBrokers;
using V9.Services.Authorization.Data.Entities;
using V9.Services.Authorization.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var factory = new ConnectionFactory() {HostName = "localhost"};

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
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

if (builder.Environment.IsDevelopment())
    builder.AddPersistence<UserDbContext>();
else
    builder.Services.AddDbContext<UserDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetValue<string>("EnvConnection")));


// Mediator 
builder.Services.AddMediatorAndFluentValidation(new[] {typeof(User).Assembly});

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
app.UseJwtAuthentication();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}