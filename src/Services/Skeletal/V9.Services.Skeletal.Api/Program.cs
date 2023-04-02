using System.Text.Json.Serialization;
using RabbitMQ.Client;
using Serilog;
using V9.Application.Extensions;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Infrastructure.Extensions;
using V9.Infrastructure.MessageBrokers;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Extensions;
using V9.Services.Skeletal.Data.Persistence;
using V9.Services.Skeletal.Data.Repositories;

var factory = new ConnectionFactory() {HostName = "localhost"};

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.Services.AddTransient<IFiberRepository, FiberRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IMuscleRepository, MuscleRepository>();
builder.Services.AddTransient<IMuscleGroupRepository, MuscleGroupRepository>();
builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();

// Data layer
builder.AddPersistence<SkeletalDbContext>();

// Mediator 
builder.Services.AddMediatorAndFluentValidation(new[] {typeof(Muscle).Assembly});
builder.Services.AddEventHandlers(typeof(SkeletalDbContext).Assembly);

builder.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePersistence<SkeletalDbContext>();
await app.InitializeDatabase();
await app.SynchronizeDataFromRemotes();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEventHandlers();
app.MapControllers();

app.Run();

public partial class Program
{
}
