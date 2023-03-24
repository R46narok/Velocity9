using System.Drawing;
using Emgu.CV;

namespace ZeroGravity.Services.Coach.DeepLearning.Extensions;

public static class MatExtensions
{
    public static byte[] ToByteArrayWithInputResolution(this Mat image, int dim)
    {
        var resized = image.Clone();
        CvInvoke.Resize(image, resized, new Size(dim, dim));
        var data = resized.GetData();

        return Flatten(
            (byte[,,]) data,
            dim, dim,
            image.NumberOfChannels);
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