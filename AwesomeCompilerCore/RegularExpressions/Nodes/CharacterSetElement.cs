using AwesomeCompilerCore.Common;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

public abstract class CharacterSetElement {}

[DebuggerDisplay("{ToString()}")]
public class SingleCharacterSetElement(char value) : CharacterSetElement, IEquatable<SingleCharacterSetElement> 
{
    public char Value { get; } = value;

    public override string ToString() => Value.CharToString();

    #region Equals
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

    public static bool operator ==(SingleCharacterSetElement left, SingleCharacterSetElement right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(SingleCharacterSetElement left, SingleCharacterSetElement right)
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
            hash = (hash * 16777619) ^ Value.GetHashCode();
            return hash;
        }
    }
    #endregion
}

[DebuggerDisplay("{ToString()}")]
public class RangeCharacterSetElement(char start, char end) : CharacterSetElement, IEquatable<RangeCharacterSetElement>
{
    public char Start { get; } = start;
    public char End { get; } = end;

    public override string ToString() => $"{Start.CharToString()}-{End.CharToString()}";

    #region Equals
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

    public static bool operator ==(RangeCharacterSetElement left, RangeCharacterSetElement right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(RangeCharacterSetElement left, RangeCharacterSetElement right)
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
            hash = (hash * 16777619) ^ Start.GetHashCode();
            hash = (hash * 16777619) ^ End.GetHashCode();
            return hash;
        }
    }
    #endregion
}