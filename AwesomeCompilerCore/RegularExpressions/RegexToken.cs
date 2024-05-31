using AwesomeCompilerCore.Common;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions;

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
    Any,              // .
    Character,        // Anything else
    EndOfInput
}

[DebuggerDisplay("{ToString()}")]
public class RegexToken(RegexTokenType type, char? value = null) : IEquatable<RegexToken>
{
    public RegexTokenType Type { get; init; } = type;
    public char? Value { get; init; } = value;

    public override string ToString()
    {
        return $"Type={Type}{(Value == null ? "" : ",Char=" + Value?.CharToString())}";
    }

    #region Equals
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

    public override int GetHashCode()
    {
        // From here: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
        unchecked // Overflow is fine, just wrap
        {
            int hash = (int)2166136261;
            // Suitable nullity checks etc, of course :)
            hash = (hash * 16777619) ^ Type.GetHashCode();
            hash = (hash * 16777619) ^ Value.GetHashCode();
            return hash;
        }
    }
    #endregion
}