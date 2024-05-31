using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

[DebuggerDisplay("Plus node [+]")]
public class PlusRegexNode : RegexNode, IEquatable<PlusRegexNode>
{
    public RegexNode Child { get; private set; }

    public PlusRegexNode(RegexNode child)
    {
        Child = child;
        Child.Parent = this;
    }

    public override bool Match(List<char> input)
    {
        var result = false;
        while (true)
        {
            if (Child.Match(input))
                result = true;
            else
                break;
        }
        return result;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(PlusRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PlusRegexNode);
    }

    public static bool operator ==(PlusRegexNode left, PlusRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(PlusRegexNode left, PlusRegexNode right)
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