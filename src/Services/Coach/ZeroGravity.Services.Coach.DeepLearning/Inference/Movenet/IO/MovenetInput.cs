using Microsoft.ML.OnnxRuntime.Tensors;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.IO;

public class MovenetInput
{
    public DenseTensor<byte>? Image { get; }

    private const int ImageWidth = 256;
    private const int ImageHeight = 256;
    private const int Channels = 3;
    private static int[] Dim => new[] {1, ImageWidth, ImageHeight, Channels};

    public MovenetInput(byte[] image)
    {
        Image = new DenseTensor<byte>(image, Dim);
    }

    public MovenetInput()
    {
        
    }
}