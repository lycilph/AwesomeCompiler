using AwesomeCompilerCore.RegularExpressions.Nodes;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions;

[DebuggerDisplay("{Pattern}")]
public class Regex : RegexNode, IEquatable<Regex>
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

    #region Equals
    public bool Equals(Regex? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Pattern == other.Pattern &&
               Root.Equals(other.Root);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Regex);
    }

    public static bool operator ==(Regex left, Regex right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Regex left, Regex right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        // From here: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
        unchecked // Overflow is fine, just wrap
        {
            int hash = (int)2166136261;
            // Suitable nullity checks etc, of course :)
            hash = (hash * 16777619) ^ Id.GetHashCode();
            hash = (hash * 16777619) ^ Pattern.GetHashCode();
            hash = (hash * 16777619) ^ Root.GetHashCode();
            return hash;
        }
    }
    #endregion
}
