using Microsoft.ML.OnnxRuntime.Tensors;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly.IO;

public class AnomalyInput
{
    private int[] Dim => new [] {-1, 10, 51};
    public DenseTensor<float>? KeyPoints { get; }

    public AnomalyInput(float[] keyPoints)
    {
        KeyPoints = new DenseTensor<float>(keyPoints, Dim);
    }
    
    public AnomalyInput(DenseTensor<float> keyPoints)
    {
        KeyPoints = keyPoints;
    }
    
    public AnomalyInput()
    {
        
    }
}