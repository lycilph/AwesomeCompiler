using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions;

public abstract class RegexNode
{
    private static int counter = 0;
    
    public int Id { get; } = counter++;

    public abstract void Accept(IVisitor visitor);
    public abstract R Accept<R>(IVisitor<R> visitor);
}

public class CharacterNode(char value) : RegexNode
{
    public char Value { get; } = value;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public class AlternationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public class ConcatenationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public class StarNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public class PlusNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}

public class OptionalNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
}