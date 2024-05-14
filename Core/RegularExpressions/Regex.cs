namespace Core.RegularExpressions;

public class Regex
{
    private readonly string _pattern;
    private readonly Node _root;

    public Regex(string pattern)
    {
        _pattern = pattern;

        var parser = new RegexParser(_pattern);
        _root = parser.Parse();
    }

    public bool IsMatch(string input)
    {
        return _root.IsMatch(input.ToList());
    }

    public Node GetRoot() => _root;

    public static bool IsMatch(string input, string pattern) 
    {
        return new Regex(input).IsMatch(pattern);
    }
}
