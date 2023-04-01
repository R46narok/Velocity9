using Microsoft.ML.OnnxRuntime.Tensors;

#nullable disable

namespace V9.Services.Coach.DeepLearning.Inference.Movenet.IO;

public class MovenetOutput
{
    public DenseTensor<float> KeyPoints { get; }

    public MovenetOutput(DenseTensor<float> keyPoints)
    {
        KeyPoints = keyPoints;
    }

    public MovenetOutput()
    {
        
    }
}