namespace Core.NFA;

public class Transition
{
    // An epsilon transition is an "empty" transition not consuming any input
    public bool Epsilon { get; set; } = false;
    public bool MatchAny { get; set; } = false;
    public HashSet<char> Chars { get; set; } = [];
    public Node FromNode { get; set; } = null!;
    public Node ToNode { get; set; } = null!;

    public static Transition CreateEmpty(Node from, Node to)
    {
        return new Transition()
        {
            FromNode = from,
            ToNode = to,
            Epsilon = true
        };
    }

    public static Transition CreateMatchAny(Node from, Node to)
    {
        return new Transition()
        {
            FromNode = from,
            ToNode = to,
            MatchAny = true
        };
    }

    public static Transition Create(Node from, Node to, char c)
    {
        var result = new Transition()
        {
            FromNode = from,
            ToNode = to
        };
        result.Chars.Add(c);

        return result;
    }

    public static Transition Create(Node from, Node to, HashSet<char> chars)
    {
        var result = new Transition()
        {
            FromNode = from,
            ToNode = to
        };
        result.Chars.UnionWith(chars);

        return result;
    }
}
