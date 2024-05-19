namespace Core.RegularExpressions;

public class Regex
{
    private readonly string _pattern;

    public RegexNode Node { get; private set; }

    public Regex(string pattern)
    {
        _pattern = pattern;

        // Reset regex node counter
        RegexNode.ResetCounter();

        // Tokenize string
        var tokens = RegexTokenizer.Tokenize(_pattern);

        // Parse tokens
        Node = RegexParser.Parse(tokens);

        // Simplify tree
    }
}
