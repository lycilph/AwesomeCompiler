using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Character node [{ToString()}]")]
public class CharacterNode(char value) : RegexNode, IEquatable<CharacterNode>
{
    public char Value { get; } = value;
    public override string ToString()
    {
        return Value switch
        {
            '\\' => @"\\",
            '\n' => @"\\n",
            '\r' => @"\\r",
            _ => Value.ToString(),
        };
    }
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode) { }

    public bool Equals(CharacterNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id && Value == other.Value;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + Value.GetHashCode();
        return hash;
    }
}
