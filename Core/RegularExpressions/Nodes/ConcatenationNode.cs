using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Concatetion node [.]")]
public class ConcatenationNode : RegexNode, IEquatable<ConcatenationNode>
{
    public RegexNode Left { get; }
    public RegexNode Right { get; }

    public ConcatenationNode(RegexNode left, RegexNode right)
    {
        Left = left;
        Left.Parent = this;

        Right = right;
        Right.Parent = this;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode) { }

    public bool Equals(ConcatenationNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Left.Equals(other.Left) &&
               Right.Equals(other.Right);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as ConcatenationNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + Left.GetHashCode();
        hash = hash * 23 + Right.GetHashCode();
        return hash;
    }
}
