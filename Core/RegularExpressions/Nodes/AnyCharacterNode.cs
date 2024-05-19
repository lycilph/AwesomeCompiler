using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Any Character node [.]")]
public class AnyCharacterNode : RegexNode, IEquatable<AnyCharacterNode>
{
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode) { }

    public bool Equals(AnyCharacterNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as AnyCharacterNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + ".".GetHashCode();
        return hash;
    }
}
