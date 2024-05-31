using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

[DebuggerDisplay("Any Character node [.]")]
public class AnyCharacterRegexNode : RegexNode, IEquatable<AnyCharacterRegexNode>
{
    public override bool Match(List<char> input)
    {
        if (input.Count > 0)
        {
            input.RemoveAt(0);
            return true;
        }
        return false;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(AnyCharacterRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as AnyCharacterRegexNode);
    }

    public static bool operator ==(AnyCharacterRegexNode left, AnyCharacterRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(AnyCharacterRegexNode left, AnyCharacterRegexNode right)
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
            return hash;
        }
    }
    #endregion
}
