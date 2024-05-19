using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

public abstract class RegexNode
{
    private static int counter = 0;
    public static void ResetCounter() => counter = 0;
    
    public int Id { get; } = counter++;
    public abstract void Accept(IVisitor visitor);
    public abstract R Accept<R>(IVisitor<R> visitor);
}

[DebuggerDisplay("Any Character node [.]")]
public class AnyCharacterNode : RegexNode, IEquatable<AnyCharacterNode>
{
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
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
        hash = hash * 23 + ".".GetHashCode();
        return hash;
    }
}

[DebuggerDisplay("Character node [{Value}]")]
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

[DebuggerDisplay("Character set node [{ToString()}]")]
public class CharacterSetNode(bool negate) : RegexNode, IEquatable<CharacterSetNode>
{
    public bool Negate { get; } = negate;
    public List<CharacterSetNodeElement> Elements { get; } = [];
    public void Add(CharacterSetNodeElement element) => Elements.Add(element);
    public void Add(char c) => Add(new SingleCharacterSetElement(c));
    public void Add(char s, char e) => Add(new RangeCharacterSetElement(s, e));
    public override string ToString()
    {
        var negate = Negate ? "^" : "";
        var elements = string.Join("", Elements.Select(e => e.ToString()));
        return negate+elements;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    public bool Equals(CharacterSetNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Negate == other.Negate && 
               Elements.SequenceEqual(other.Elements);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterSetNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + Negate.GetHashCode();
        foreach (var e in Elements)
            hash = hash * 23 + e.GetHashCode();
        return hash;
    }
}

public abstract class CharacterSetNodeElement {}

[DebuggerDisplay("{Value}")]
public class SingleCharacterSetElement(char value) : CharacterSetNodeElement, IEquatable<SingleCharacterSetElement>
{
    public char Value { get; } = value;
    public override string ToString() => Value.ToString();
    public bool Equals(SingleCharacterSetElement? other)
    {
        if (other is null)
            return false;

        return Value == other.Value;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as SingleCharacterSetElement);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Value.GetHashCode();
        return hash;
    }
}

[DebuggerDisplay("{Start}-{End}")]
public class RangeCharacterSetElement(char start, char end) : CharacterSetNodeElement, IEquatable<RangeCharacterSetElement>
{
    public char Start { get; } = start;
    public char End { get; } = end;
    public override string ToString() => $"{Start}-{End}";
    public bool Equals(RangeCharacterSetElement? other)
    {
        if (other is null)
            return false;

        return Start == other.Start && End == other.End;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as RangeCharacterSetElement);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Start.GetHashCode();
        hash = hash * 23 + End.GetHashCode();
        return hash;
    }
}

[DebuggerDisplay("Alternation node [|]")]
public class AlternationNode(RegexNode left, RegexNode right) : RegexNode, IEquatable<AlternationNode>
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    public bool Equals(AlternationNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Left.Equals(other.Left) &&
               Right.Equals(other.Right);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as AlternationNode);
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

[DebuggerDisplay("Concatetion node [.]")]
public class ConcatenationNode(RegexNode left, RegexNode right) : RegexNode, IEquatable<ConcatenationNode>
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
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

[DebuggerDisplay("Star node [*]")]
public class StarNode(RegexNode child) : RegexNode, IEquatable<StarNode>
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
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

[DebuggerDisplay("Plus node [+]")]
public class PlusNode(RegexNode child) : RegexNode, IEquatable<PlusNode>
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    public bool Equals(PlusNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as PlusNode);
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

[DebuggerDisplay("Optional node [?]")]
public class OptionalNode(RegexNode child) : RegexNode, IEquatable<OptionalNode>
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    public bool Equals(OptionalNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as OptionalNode);
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