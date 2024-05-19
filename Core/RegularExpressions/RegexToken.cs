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

public class RegexToken
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
}
