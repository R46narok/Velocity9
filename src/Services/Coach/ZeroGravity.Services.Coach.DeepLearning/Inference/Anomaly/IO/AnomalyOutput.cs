using Microsoft.ML.OnnxRuntime.Tensors;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly.IO;

public class AnomalyOutput
{
    public DenseTensor<float>? KeyPoints { get; }

    public AnomalyOutput(Tensor<float> keyPoints)
    {
        KeyPoints = keyPoints.ToDenseTensor();
    }
    
    public AnomalyOutput()
    {
        
    }
}