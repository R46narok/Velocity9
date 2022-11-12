using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Skeletal.Data;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Extensions;
using ZeroGravity.Services.Skeletal.Data.Persistence;
using ZeroGravity.Services.Skeletal.Data.Repositories;

var factory = new ConnectionFactory() {HostName = "localhost"};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
builder.Services.AddTransient<IFiberRepository, FiberRepository>();
builder.Services.AddTransient<IMuscleRepository, MuscleRepository>();
builder.Services.AddTransient<IMuscleGroupRepository, MuscleGroupRepository>();

// Data layer
builder.AddPersistence<MuscleDbContext>();

// Mediator 
builder.Services.AddMediatorAndVluentValidation(new[] {typeof(Muscle).Assembly});

builder.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePersistence<MuscleDbContext>();
await app.InitializeDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
