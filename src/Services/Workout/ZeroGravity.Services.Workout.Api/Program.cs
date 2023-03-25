using System.Text.Json.Serialization;
using RabbitMQ.Client;
using Serilog;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Infrastructure.Extensions;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Extensions;
using ZeroGravity.Services.Workout.Data.Persistence;
using ZeroGravity.Services.Workout.Data.Repositories;
using ZeroGravity.Services.Workout.DeepLearning.Data;
using ZeroGravity.Services.Workout.DeepLearning.Models;

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

builder.AddJwtAuthentication();

builder.AddPersistence<WorkoutDbContext>();

builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IMuscleRepository, MuscleRepository>();
builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPreferencesRepository, PreferencesRepository>();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.Services.AddMediatorAndVluentValidation(new[] {typeof(Workout).Assembly});
builder.Services.AddEventHandlers(typeof(WorkoutDbContext).Assembly);
builder.Services.AddTransient<IInferenceModel<WorkoutModelInput, WorkoutModelOutput>, WorkoutModel>(
    opt => new WorkoutModel("encoder.onnx", "e_decoder.onnx",
    "Bench press,Incline bench press,Triceps iso,Shoulder raise,Paused dips,Chest flies,Weighted dips,Rings,Shoulder press,HSPU,Decline bench press"
            .Split(',').ToList()));

var app = builder.Build();
app.UsePersistence<WorkoutDbContext>();
await app.SynchronizeDataFromRemotes();

app.UseEventHandlers();
app.UseJwtAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program
{
}