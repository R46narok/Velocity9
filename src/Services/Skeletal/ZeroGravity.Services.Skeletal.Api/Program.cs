using System.Text.Json.Serialization;
using RabbitMQ.Client;
using Serilog;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.Extensions;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Extensions;
using ZeroGravity.Services.Skeletal.Data.Persistence;
using ZeroGravity.Services.Skeletal.Data.Repositories;

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
builder.Services.AddMediatorAndVluentValidation(new[] {typeof(Muscle).Assembly});
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
