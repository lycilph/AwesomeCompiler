using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

[DebuggerDisplay("Star node [*]")]
public class StarRegexNode : RegexNode, IEquatable<StarRegexNode>
{
    public RegexNode Child { get; private set; }

    public StarRegexNode(RegexNode child)
    {
        Child = child;
        Child.Parent = this;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(StarRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StarRegexNode);
    }

    public static bool operator ==(StarRegexNode left, StarRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(StarRegexNode left, StarRegexNode right)
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
            hash = (hash * 16777619) ^ Child.GetHashCode();
            return hash;
        }
    }
    #endregion
}