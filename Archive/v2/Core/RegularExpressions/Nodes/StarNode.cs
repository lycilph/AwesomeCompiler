using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Star node [*]")]
public class StarNode : RegexNode, IEquatable<StarNode>
{
    public RegexNode Child { get; private set; }

    public StarNode(RegexNode child)
    {
        Child = child;
        Child.Parent = this;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode)
    {
        if (Child == oldNode)
            Child = newNode;
    }

    public bool Equals(StarNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as StarNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + Child.GetHashCode();
        return hash;
    }
}
