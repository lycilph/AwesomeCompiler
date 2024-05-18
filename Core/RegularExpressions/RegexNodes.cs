namespace Core.RegularExpressions;

public abstract class RegexNode {}

public class CharacterNode(char value) : RegexNode
{
    public char Value { get; } = value;
}

public class AlternationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
}

public class ConcatenationNode(RegexNode left, RegexNode right) : RegexNode
{
    public RegexNode Left { get; } = left;
    public RegexNode Right { get; } = right;
}

public class StarNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
}

public class PlusNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
}

public class OptionalNode(RegexNode child) : RegexNode
{
    public RegexNode Child { get; } = child;
}