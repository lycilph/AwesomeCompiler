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

    public Node GetRoot() => _root;
}
