namespace AwesomeCompilerCore.Grammars;

public enum GrammarTokenType
{
    String,           // regex
    Whitespace,       // regex
    Comment,          // regex
    Identifier,       // regex
    LeftParenthesis,  // (
    RightParenthesis, // )
    Alternation,      // |
    Star,             // *
    Plus,             // +
    Optional,         // ?
    Colon,            // :
    Semicolon,        // ;
    EndOfInput
}

public class GrammarToken(GrammarTokenType type, string lexeme)
{
    public GrammarTokenType Type { get; init; } = type;
    public string Lexeme { get; init; } = lexeme;
}
