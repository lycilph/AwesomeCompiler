namespace AwesomeCompilerCore.RegularExpressions;

public class RegexTokenizer
{
    private readonly string input;
    private int position;

    public RegexTokenizer(string input)
    {
        this.input = input;
        position = 0;
    }

    private void Advance() => position++;

    public List<RegexToken> Run()
    {
        var tokens = new List<RegexToken>();

        char current;
        while (position < input.Length) 
        { 
            current = input[position];

            switch (current)
            {
                case '(':
                    tokens.Add(new RegexToken(RegexTokenType.LeftParenthesis));
                    Advance();
                    break;
                case ')':
                    tokens.Add(new RegexToken(RegexTokenType.RightParenthesis));
                    Advance();
                    break;
                case '[':
                    tokens.Add(new RegexToken(RegexTokenType.LeftBracket));
                    Advance();
                    break;
                case ']':
                    tokens.Add(new RegexToken(RegexTokenType.RightBracket));
                    Advance();
                    break;
                case '-':
                    tokens.Add(new RegexToken(RegexTokenType.Hyphen));
                    Advance();
                    break;
                case '^':
                    tokens.Add(new RegexToken(RegexTokenType.Negation));
                    Advance();
                    break;
                case '|':
                    tokens.Add(new RegexToken(RegexTokenType.Alternation));
                    Advance();
                    break;
                case '*':
                    tokens.Add(new RegexToken(RegexTokenType.Star));
                    Advance();
                    break;
                case '+':
                    tokens.Add(new RegexToken(RegexTokenType.Plus));
                    Advance();
                    break;
                case '?':
                    tokens.Add(new RegexToken(RegexTokenType.Optional));
                    Advance();
                    break;
                case '.':
                    tokens.Add(new RegexToken(RegexTokenType.Any));
                    Advance();
                    break;
                case '\\':
                    tokens.Add(HandleEscapeCharacter());
                    break;
                default:
                    tokens.Add(new RegexToken(RegexTokenType.Character, current));
                    Advance();
                    break;
            }
        }

        tokens.Add(new RegexToken(RegexTokenType.EndOfInput));

        return tokens;
    }

    private RegexToken HandleEscapeCharacter()
    {
        Advance();
        if (position >= input.Length)
            throw new Exception("Incomplete escape sequence");

        var current = input[position];
        Advance();

        return current switch
        {
            't' => new RegexToken(RegexTokenType.Character, '\t'),
            'r' => new RegexToken(RegexTokenType.Character, '\r'),
            'n' => new RegexToken(RegexTokenType.Character, '\n'),
            _ => new RegexToken(RegexTokenType.Character, current),
        };
    }

    public static List<RegexToken> Tokenize(string input)
    {
        var tokenizer = new RegexTokenizer(input);
        return tokenizer.Run();
    }
}
