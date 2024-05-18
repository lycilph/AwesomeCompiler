namespace Core.RegularExpressions;

public class RegexParser
{
    private readonly List<RegexToken> _tokens;
    private int _position;

    public RegexParser(List<RegexToken> tokens)
    {
        _tokens = tokens;
        _position = 0;
    }

    private void Advance() => _position++;
    private RegexToken Current => _tokens[_position];

    private void Match(RegexTokenType type)
    {
        if (Current.Type != type)
            throw new InvalidDataException($"Expected {type} but got {Current.Type}");
        Advance();
    }

    public RegexNode Parse()
    {
        return ParseAlternation();
    }

    private RegexNode ParseAlternation()
    {
        var node = ParseConcatenation();

        while (Current.Type == RegexTokenType.Alternation)
        {
            Advance();
            var right = ParseConcatenation();
            node = new AlternationNode(node, right);
        }

        return node;
    }

    private RegexNode ParseConcatenation()
    {
        var node = ParseStar();

        while (Current.Type == RegexTokenType.Character || Current.Type == RegexTokenType.LeftParenthesis)
        {
            var right = ParseStar();
            node = new ConcatenationNode(node, right);
        }

        return node;
    }

    private RegexNode ParseStar()
    {
        var node = ParsePlus();

        while (Current.Type == RegexTokenType.Star)
        {
            Advance();
            node = new StarNode(node);
        }

        return node;
    }

    private RegexNode ParsePlus()
    {
        var node = ParseOptional();

        while (Current.Type == RegexTokenType.Plus)
        {
            Advance();
            node = new PlusNode(node);
        }

        return node;
    }

    private RegexNode ParseOptional()
    {
        var node = ParsePrimary();

        while (Current.Type == RegexTokenType.Optional)
        {
            Advance();
            node = new OptionalNode(node);
        }

        return node;
    }

    private RegexNode ParsePrimary()
    {
        switch (Current.Type) 
        {
            case RegexTokenType.Character:
                var charNode = new CharacterNode(Current.Value!.Value);
                Advance();
                return charNode;
            case RegexTokenType.LeftParenthesis:
                Advance();
                var node = ParseAlternation();
                Match(RegexTokenType.RightParenthesis);
                return node;
            default:
                throw new InvalidDataException($"Unknown token {Current.Type}");
        }
    }
}
