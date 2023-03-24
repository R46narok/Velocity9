using Emgu.CV.Structure;

namespace ZeroGravity.Services.Coach.DeepLearning.Constants;

public class Colors
{
    public static Dictionary<char, Rgb> Library = new()
    {
        {'m', new Rgb(88, 214, 141)},
        {'c', new Rgb(220, 118, 51)},
        {'y', new Rgb(191, 182, 174)}
    };
}