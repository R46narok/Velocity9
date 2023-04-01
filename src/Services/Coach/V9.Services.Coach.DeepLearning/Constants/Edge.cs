using Emgu.CV.Structure;

namespace V9.Services.Coach.DeepLearning.Constants;

public class Edge
{
    public static Dictionary<(int, int), Rgb> ToColor = new()
    {
        {(0, 1), Colors.Library['m']},
        {(0, 2), Colors.Library['c']},
        {(1, 3), Colors.Library['m']},
        {(2, 4), Colors.Library['c']},
        {(0, 5), Colors.Library['m']},
        {(0, 6), Colors.Library['c']},
        {(5, 7), Colors.Library['m']},
        {(7, 9), Colors.Library['m']},
        {(6, 8), Colors.Library['c']},
        {(8, 10), Colors.Library['c']},
        {(5, 6), Colors.Library['y']},
        {(5, 11), Colors.Library['m']},
        {(6, 12), Colors.Library['c']},
        {(11, 12), Colors.Library['y']},
        {(11, 13), Colors.Library['m']},
        {(13, 15), Colors.Library['m']},
        {(12, 14), Colors.Library['c']},
        {(14, 16), Colors.Library['c']}
    };
}