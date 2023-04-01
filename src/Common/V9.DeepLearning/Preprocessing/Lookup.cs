namespace V9.DeepLearning.Preprocessing;

public static class Lookup
{

    public static Dictionary<string, int> For(string[] unique)
    {
        return unique
            .Select((t, i) => new {Token = t, Index = i})
            .ToDictionary(x => x.Token, x => x.Index);
    }

    public static Dictionary<int, string> ReverseFor(string[] unique)
    {
        return unique
            .Select((t, i) => new {Token = t, Index = i})
            .ToDictionary(x => x.Index, x => x.Token);
    }

    private static int StringComparison(string x, string y)
    {
        if (x == y)
            return 0;
        if (x.StartsWith("_") && !y.StartsWith("_"))
            return 1;
        if (!x.StartsWith("_") && y.StartsWith("_"))
            return -1;
        return x.CompareTo(y);
    }

}