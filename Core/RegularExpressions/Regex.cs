using Core.RegularExpressions.Nodes;

namespace Core.RegularExpressions;

public class Regex : ISimplifiable
{
    private readonly string _pattern;

    public ISimplifiable? Parent { get; set; }
    public RegexNode Node { get; private set; }

    public Regex(string pattern)
    {
        _pattern = pattern;

        // Tokenize string
        var tokens = RegexTokenizer.Tokenize(_pattern);

        // Parse tokens
        Node = RegexParser.Parse(tokens);
        Node.Parent = this;
    }

    public void Replace(RegexNode oldNode, RegexNode newNode)
    {
        if (Node == oldNode)
            Node = newNode;
        Node.Parent = this;
    }
}
