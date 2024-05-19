using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

public abstract class CharacterSetElement { }

[DebuggerDisplay("{Value}")]
public class SingleCharacterSetElement(char value) : CharacterSetElement, IEquatable<SingleCharacterSetElement>
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
public class RangeCharacterSetElement(char start, char end) : CharacterSetElement, IEquatable<RangeCharacterSetElement>
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