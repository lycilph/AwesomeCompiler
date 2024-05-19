using Core.RegularExpressions.Algorithms;
using System.Diagnostics;
using System.Xml.Linq;

namespace Core.RegularExpressions;

public abstract class RegexNode
{
    private static int counter = 0;

    public static void ResetCount() => counter = 0;
    
    public int Id { get; } = counter++;

    public abstract void Accept(IVisitor visitor);
    public abstract R Accept<R>(IVisitor<R> visitor);
}

[DebuggerDisplay("Character node [{Value}]")]
public class CharacterNode(char value) : RegexNode
{
    public char Value { get; } = value;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

[DebuggerDisplay("Character set node [{ToString()}]")]
public class CharacterSetNode(bool negate) : RegexNode
{
    public bool Negate { get; } = negate;
    public List<CharacterSetNodeElement> Elements { get; } = [];
    public void Add(CharacterSetNodeElement element) => Elements.Add(element);
    public override string ToString()
    {
        var negate = Negate ? "^" : "";
        var elements = string.Join("", Elements.Select(e => e.ToString()));
        return negate+elements;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public abstract class CharacterSetNodeElement {}

[DebuggerDisplay("{Value}")]
public class SingleCharacterSetElement(char value) : CharacterSetNodeElement
{
    public char Value { get; } = value;
    public override string ToString() => Value.ToString();
}

[DebuggerDisplay("{Start}-{End}")]
public class RangeCharacterSetElement(char start, char end) : CharacterSetNodeElement
{
    public char Start { get; } = start;
    public char End { get; } = end;
    public override string ToString() => $"{Start}-{End}";
}

[DebuggerDisplay("Alternation node [|]")]
public class AlternationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

[DebuggerDisplay("Concatetion node [.]")]
public class ConcatenationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

[DebuggerDisplay("Star node [*]")]
public class StarNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

[DebuggerDisplay("Plus node [+]")]
public class PlusNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

[DebuggerDisplay("Optional node [?]")]
public class OptionalNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}