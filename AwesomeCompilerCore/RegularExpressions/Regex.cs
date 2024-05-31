using AwesomeCompilerCore.RegularExpressions.Nodes;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions;

[DebuggerDisplay("{Pattern}")]
public class Regex(string pattern, RegexNode root) : RegexNode
{
    public string Pattern { get; set; } = pattern;
    public RegexNode Root { get; private set; } = root;

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion
}
