using System.Text.Json.Serialization;
using RabbitMQ.Client;
using Serilog;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.Extensions;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Extensions;
using ZeroGravity.Services.Workout.Data.Persistence;
using ZeroGravity.Services.Workout.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var factory = new ConnectionFactory() {HostName = "localhost"};

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence<WorkoutDbContext>();

builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IMuscleRepository, MuscleRepository>();
builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.Services.AddMediatorAndVluentValidation(new[] {typeof(Workout).Assembly});
builder.Services.AddEventHandlers(typeof(WorkoutDbContext).Assembly);

var app = builder.Build();
app.UsePersistence<WorkoutDbContext>();
await app.SynchronizeDataFromRemotes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
