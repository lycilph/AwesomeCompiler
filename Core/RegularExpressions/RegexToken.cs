namespace Core.RegularExpressions;

public enum RegexTokenType
{
    LeftParenthesis,  // (
    RightParenthesis, // )
    LeftBracket,      // [
    RightBracket,     // ]
    Hyphen,           // -
    Negation,         // ^
    Alternation,      // |
    Star,             // *
    Plus,             // +
    Optional,         // ?
    Character,        // Anything else
    EndOfInput
}

public class RegexToken : IEquatable<RegexToken>
{
    public RegexTokenType Type { get; }
    public char? Value { get; }

    public RegexToken(RegexTokenType type, char? value = null)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Type}{(Value == null?"":" - "+Value)}";
    }

    public bool Equals(RegexToken? other)
    {
        if (other is null)
            return false;

        return Type == other.Type && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as RegexToken);
    }

    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Type.GetHashCode();
        hash = hash * 23 + (Value?.GetHashCode() ?? 0);
        return hash;
    }

    public static bool operator ==(RegexToken left, RegexToken right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(RegexToken left, RegexToken right)
    {
        return !(left == right);
    }
}
