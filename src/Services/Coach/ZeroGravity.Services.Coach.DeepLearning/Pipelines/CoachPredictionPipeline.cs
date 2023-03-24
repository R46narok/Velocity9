using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.ML.OnnxRuntime.Tensors;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Constants;
using ZeroGravity.Services.Coach.DeepLearning.Extensions;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet;

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
        var features = new DenseTensor<float>(new[] {1, TimeStep, Features});

        PredictKeyPoints(capture, ref frame, count, ref features);

        var reconstructed = RunAnomalyOnValidKeyPoints(ref features);
        capture.Set(CapProp.PosFrames, 0.0);

        DrawPredictedKeyPoints(capture, ref frame, count, ref reconstructed);
    }

    private void PredictKeyPoints(
        VideoCapture capture, ref Mat frame, 
        int count, 
        ref DenseTensor<float> features)
    {
        int idx = 0;
        for (int i = 0; i < count; i++)
        {
            var ok = capture.Read(frame);
            if (i % 10 != 0) continue;
            if (idx == TimeStep) break;

            if (ok && !frame.IsEmpty)
            {
                RunMovenetOnValidFrame(frame, idx, ref features);
                ++idx;
            }
        }
    }

    private void DrawPredictedKeyPoints(
        VideoCapture capture, ref Mat frame,
        int count, 
        ref DenseTensor<float> reconstructed)
    {
        int idx = 0;
        for (int i = 10; i < count; i++)
        {
            var ok = capture.Read(frame);
            frame = frame.Pad(InputResolution);
            if (i % 10 != 0) continue;
            if (idx == TimeStep) break;

            if (ok && !frame.IsEmpty)
            {
                DrawPose(frame, reconstructed!, idx);
                ++idx;
            }
        }
    }
    
    private void RunMovenetOnValidFrame(Mat frame, int idx, ref DenseTensor<float> features)
    {
        var raw = MovenetPreprocess(frame);
        var keyPoints = _movenet
            .Predict(new(raw))
            .KeyPoints;

        keyPoints = MovenetPostprocess(keyPoints);

        for (int j = 0; j < Features; ++j)
            features[0, idx, j] = keyPoints[0, 0, j];
    }

    private byte[] MovenetPreprocess(Mat frame)
    {
        var padded = frame.Pad(InputResolution);
        return padded.GetRawData();
    }

    private DenseTensor<float> MovenetPostprocess(DenseTensor<float> tensor)
    {
        return tensor
            .Reshape(new[] {1, 1, Features})
            .ToDenseTensor();
    }

    private DenseTensor<float> RunAnomalyOnValidKeyPoints(ref DenseTensor<float> features)
    {
        return _anomaly
            .Predict(new(features))
            .KeyPoints!
            .Reshape(new[] {10, 17, 3})
            .ToDenseTensor();
    }

    private void DrawPose(Mat frame, DenseTensor<float> keyPoints, int idx)
    {
        PreprocessKps(keyPoints, idx);
        for (int i = 0; i < 17; ++i)
        {
            var x = keyPoints[idx, i, 0];
            var y = keyPoints[idx, i, 1];
            var s = keyPoints[idx, i, 2];

            if (s > 0.2f)
            {
                CvInvoke.Circle(frame, new((int) x, (int) y), 2, new(185, 128, 41), 4);
            }

            foreach (KeyValuePair<(int, int), Rgb> entry in Edge.ToColor)
            {
                int start = entry.Key.Item1;
                int end = entry.Key.Item2;

                var x1 = (int) keyPoints[idx, start, 0];
                var y1 = (int) keyPoints[idx, start, 1];
                var x2 = (int) keyPoints[idx, end, 0];
                var y2 = (int) keyPoints[idx, end, 1];

                CvInvoke.Line(frame, new(x1, y1), new(x2, y2),
                    entry.Value.MCvScalar);
            }
        }
    }

    private void PreprocessKps(DenseTensor<float> keyPoints, int idx)
    {
        for (int i = 0; i < 17; ++i)
        {
            var temp = keyPoints[idx, i, 1];
            keyPoints[idx, i, 1] = keyPoints[idx, i, 0] * 256;
            keyPoints[idx, i, 0] = temp * 256;
        }
    }
}