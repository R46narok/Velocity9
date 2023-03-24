using System.Drawing;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using ZeroGravity.Services.Coach.DeepLearning.Extensions;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.Impl;
using ZeroGravity.Services.Coach.DeepLearning.Pipelines;

var inference = new MovenetInference("thunder.onnx");
var anomaly = new AnomalyInference("anomaly.onnx");

var pipeline = new CoachPredictionPipeline(inference, anomaly);
pipeline.Run("1.avi");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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