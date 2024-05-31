using AwesomeCompilerCore.RegularExpressions.Nodes;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions;

[DebuggerDisplay("{Pattern}")]
public class Regex : RegexNode
{
    public string Pattern { get; private set; } = string.Empty;
    public RegexNode Root { get; private set; }

    public Regex(RegexNode root)
    {
        Root = root;
    }
    public Regex(string pattern)
    {
        Pattern = pattern;

        // Tokenize string
        var tokens = RegexTokenizer.Tokenize(Pattern);
        // Parse tokens
        Root = RegexParser.Parse(tokens);
    }
    public Regex(string pattern, RegexNode root)
    {
        Pattern = pattern;
        Root = root;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion
}
