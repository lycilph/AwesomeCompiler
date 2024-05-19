

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
    private RegexToken Peek() => _tokens[_position+1];
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

        while (Current.Type == RegexTokenType.Character || 
               Current.Type == RegexTokenType.LeftParenthesis || 
               Current.Type == RegexTokenType.LeftBracket)
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
            case RegexTokenType.LeftBracket:
                Advance();
                var charSetNode = ParseCharacterSet();
                Match(RegexTokenType.RightBracket);
                return charSetNode;
            default:
                throw new InvalidDataException($"Unknown token {Current.Type}");
        }
    }

    private RegexNode ParseCharacterSet()
    {
        bool negate;
        if (Current.Type == RegexTokenType.Negation)
        {
            negate = true;
            Advance();
        }
        else
            negate = false;
        var node = new CharacterSetNode(negate);

        while (Current.Type == RegexTokenType.Character)
        {
            if (Peek().Type == RegexTokenType.Hyphen)
                node.Add(ParseRangeElement());
            else
                node.Add(ParseSingleElement());
        }

        return node;
    }

    private SingleCharacterSetElement ParseSingleElement()
    {
        var element = new SingleCharacterSetElement(Current.Value!.Value);
        Advance();
        return element;
    }

    private RangeCharacterSetElement ParseRangeElement()
    {
        var start = Current.Value!.Value;
        Advance();
        Match(RegexTokenType.Hyphen);
        var end = Current.Value!.Value;
        Advance();
        return new RangeCharacterSetElement(start, end);
    }

    public static RegexNode Parse(List<RegexToken> tokens)
    {
        var parser = new RegexParser(tokens);
        return parser.Parse();
    }
}
