using System.Drawing;
using System.Drawing.Imaging;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Storage;

namespace V9.UI.Portal.Blazor.Pages.Profile.Index.ImageProcessing;

public static class ImageStack
{
    public static byte[] FromRawImages(byte[][] imageBytes)
    {
        using var ms = new MemoryStream(imageBytes[0]);
        var initial = Image.FromStream(ms);

        var img = new Bitmap(initial.Width, initial.Height);
        img.SetResolution(initial.HorizontalResolution, initial.VerticalResolution); // set resolution to match input image
        
        using Graphics gr = Graphics.FromImage(img);
        foreach (var layer in imageBytes)
        {
            using var stream = new MemoryStream(layer);
            var image = Image.FromStream(stream);
            gr.DrawImage(image, new Point(0, 0));
        }
        img.Save("ff.png");

        return ImageToByteArray(img);
    }

    public static byte[] ImageToByteArray(Image image)
    {
        using var memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Png);
        return memoryStream.ToArray();
    }
}