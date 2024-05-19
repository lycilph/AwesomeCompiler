using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions;

public class Regex : RegexNode
{
    private readonly string _pattern;

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

    public override void Accept(IVisitor visitor)
    {
        throw new InvalidOperationException();
    }

    public override R Accept<R>(IVisitor<R> visitor)
    {
        throw new InvalidOperationException();
    }

    public override void Replace(RegexNode oldNode, RegexNode newNode)
    {
        if (Node == oldNode)
            Node = newNode;
        Node.Parent = this;
    }
}
