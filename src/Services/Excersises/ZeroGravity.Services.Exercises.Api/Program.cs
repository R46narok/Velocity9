using RabbitMQ.Client;
using ZeroGravity.Application.Extensions;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Events;
using ZeroGravity.Infrastructure.Extensions;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.MessageBrokers.RabbitMQ;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Persistence;
using ZeroGravity.Services.Exercises.Data.Repositories;
using ZeroGravity.Services.Exercises.EventHandlers;

var builder = WebApplication.CreateBuilder(args);

var factory = new ConnectionFactory {HostName = "localhost"};

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.AddPersistence<ExercisesDbContext>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IMuscleRepository, MuscleRepository>();
builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();

builder.Services.AddMediatorAndVluentValidation(new[] {typeof(ExercisesDbContext).Assembly});
builder.AddJwtAuthentication();

builder.Services.AddEventHandlers(typeof(ExercisesDbContext).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEventHandlers();
app.UsePersistence<ExercisesDbContext>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}