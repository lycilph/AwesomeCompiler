using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

[DebuggerDisplay("Concatetion node [.]")]
public class ConcatenationRegexNode : RegexNode, IEquatable<ConcatenationRegexNode>
{
    public RegexNode Left { get; private set; }
    public RegexNode Right { get; private set; }

    public ConcatenationRegexNode(RegexNode left, RegexNode right)
    {
        Left = left;
        Left.Parent = this;

        Right = right;
        Right.Parent = this;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(ConcatenationRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Left.Equals(other.Left) &&
               Right.Equals(other.Right);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ConcatenationRegexNode);
    }

    public static bool operator ==(ConcatenationRegexNode left, ConcatenationRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(ConcatenationRegexNode left, ConcatenationRegexNode right)
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
            hash = (hash * 16777619) ^ Left.GetHashCode();
            hash = (hash * 16777619) ^ Right.GetHashCode();
            return hash;
        }
    }
    #endregion
}
