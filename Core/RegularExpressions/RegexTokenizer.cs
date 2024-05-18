namespace Core.RegularExpressions;

public class RegexTokenizer
{
    private readonly string _input;
    private int _position;

    public RegexTokenizer(string input)
    {
        _input = input;
        _position = 0;
    }

    private void Advance() => _position++;

    public List<RegexToken> Tokenize()
    {
        var tokens = new List<RegexToken>();

        while (_position < _input.Length)
        {
            var current = _input[_position];

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
                default:
                    tokens.Add(new RegexToken(RegexTokenType.Character, current));
                    Advance();
                    break;
            }
        }

        tokens.Add(new RegexToken(RegexTokenType.EndOfInput));

        return tokens;
    }
}
