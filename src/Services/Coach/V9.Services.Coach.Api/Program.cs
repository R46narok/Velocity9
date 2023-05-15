using System.Text.Json.Serialization;
using V9.Services.Coach.DeepLearning.Inference.Movenet;
using RabbitMQ.Client;
using Serilog;
using V9.Application.Extensions;
using V9.Application.Infrastructure.MessageBrokers;
using V9.DeepLearning.Common;
using V9.Infrastructure.MessageBrokers;
using V9.Services.Coach.DeepLearning.Inference.Anomaly;
using V9.Services.Coach.DeepLearning.Inference.Movenet.Impl;
using V9.Services.Coach.DeepLearning.Pipelines;


var builder = WebApplication.CreateBuilder(args);

var factory = new ConnectionFactory() {HostName = "localhost"};

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddJwtAuthentication();

builder.Services.AddSingleton<IConnection>(_ => factory.CreateConnection());
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
builder.Services.AddTransient<IMovenetInference, MovenetInference>(
    opt => new MovenetInference("thunder.onnx"));
builder.Services.AddTransient<IAnomalyInference, AnomalyInference>(
    opt => new AnomalyInference("anomaly.onnx"));
builder.Services.AddTransient<IPredictionPipeline, CoachPredictionPipeline>();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtAuthentication();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

public partial class Program
{
    
}