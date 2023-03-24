using Emgu.CV;
using Emgu.CV.CvEnum;

namespace ZeroGravity.Services.Coach.DeepLearning.Extensions;

public static class MatExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="dim"></param>
    /// <returns></returns>
    public static Mat Pad(this Mat frame, int dim)
    {
        var width = frame.Width;
        var height = frame.Height;

        var ratio = Math.Min(dim / (float) width, dim / (float) height);
        var newWidth = (int) (ratio * width);
        var newHeight = (int) (ratio * height);

        var temp = new Mat();
        CvInvoke.Resize(frame, temp, new(newWidth, newHeight));

        var padWidth = (dim - newWidth) / 2;
        var padHeight = (dim - newHeight) / 2;

        CvInvoke.CopyMakeBorder(temp, frame, 
            padHeight, padHeight, 
            padWidth, padWidth, 
            BorderType.Replicate,
            new(0));
        
        CvInvoke.Resize(frame, temp, new(dim, dim));

        return temp;
    }

    private static byte[] Flatten(byte[,,] image, int width, int height, int channels)
    {
        var flattened = new byte[width * height * channels];
        
        int i = 0;
        for (int c = 0; c < channels; ++c)
        for (int w = 0; w < width; ++w)
        for (int h = 0; h < height; ++h)
            flattened[i++] = image[w, h, c];

        return flattened;
    }
}