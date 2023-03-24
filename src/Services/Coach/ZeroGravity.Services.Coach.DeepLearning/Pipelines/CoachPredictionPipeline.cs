using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.ML.OnnxRuntime.Tensors;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Extensions;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.Impl;

namespace ZeroGravity.Services.Coach.DeepLearning.Pipelines;

public class CoachPredictionPipeline : PredictionPipelineBase
{
    private readonly IMovenetInference _movenet;
    private readonly IAnomalyInference _anomaly;
    private const int InputResolution = 256;
    private const int TimeStep = 10;
    private const int Features = 3 * 17;

    public CoachPredictionPipeline(IMovenetInference movenet, IAnomalyInference anomaly)
    {
        _movenet = movenet;
        _anomaly = anomaly;
    }

    public void Run(string fileName)
    {
        using var capture = new VideoCapture(fileName);
        var count = (int) capture.Get(CapProp.FrameCount);

        var frame = new Mat();
        var features = new DenseTensor<float>(new int[] {1, TimeStep, Features});
        var idx = 0;
        
        for (int i = 0; i < count; i++)
        {
            var ok = capture.Read(frame);
            if (i % 10 != 0) continue;
            if (idx == TimeStep) break;

            if (ok && !frame.IsEmpty)
            {
                var resized = frame.ToByteArrayWithInputResolution(InputResolution);
                var keyPoints = _movenet
                    .Predict(new(resized))
                    .KeyPoints;
                
                keyPoints = keyPoints
                    .Reshape(new int [] {1, 1, Features})
                    .ToDenseTensor();

                for (int j = 0; j < Features; ++j)
                    features[0, idx, j] = keyPoints[0, 0, j];

                ++idx;
                CvInvoke.Imshow("fr", frame);
                CvInvoke.WaitKey(0);
            }
        }

        var reconstructed = _anomaly
            .Predict(new(features))
            .KeyPoints;
        
    }
}