using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerCore.RegularExpressions;

public class RegexParser
{
    private readonly List<RegexToken> tokens;
    private int position;

    public RegexParser(List<RegexToken> tokens)
    {
        this.tokens = tokens;
        position = 0;
    }

    private void Advance() => position++;
    private RegexToken Peek() => tokens[position + 1];
    private RegexToken Current => tokens[position];

    private void Match(RegexTokenType type)
    {
        if (Current.Type != type)
            throw new InvalidDataException($"Expected {type} but got {Current.Type}");
        Advance();
    }

    public RegexNode Parse()
    {
        var node = ParseAlternation();
        Match(RegexTokenType.EndOfInput);
        return node;
    }
    
    private RegexNode ParseAlternation()
    {
        var node = ParseConcatenation();

        while (Current.Type == RegexTokenType.Alternation)
        {
            Advance();
            var right = ParseConcatenation();
            node = new AlternationRegexNode(node, right);
        }

        return node;
    }

    private RegexNode ParseConcatenation()
    {
        var node = ParseStar();

        while (Current.Type == RegexTokenType.Character ||
               Current.Type == RegexTokenType.Any ||
               Current.Type == RegexTokenType.LeftParenthesis ||
               Current.Type == RegexTokenType.LeftBracket)
        {
            var right = ParseStar();
            node = new ConcatenationRegexNode(node, right);
        }

        return node;
    }

    private RegexNode ParseStar()
    {
        var node = ParsePlus();

        while (Current.Type == RegexTokenType.Star)
        {
            Advance();
            node = new StarRegexNode(node);
        }

        return node;
    }

    private RegexNode ParsePlus()
    {
        var node = ParseOptional();

        while (Current.Type == RegexTokenType.Plus)
        {
            Advance();
            node = new PlusRegexNode(node);
        }

        return node;
    }
    
    private RegexNode ParseOptional()
    {
        var node = ParsePrimary();

        while (Current.Type == RegexTokenType.Optional)
        {
            Advance();
            node = new OptionalRegexNode(node);
        }

        return node;
    }

    private RegexNode ParsePrimary()
    {
        switch (Current.Type)
        {
            case RegexTokenType.Character:
                var charNode = new CharacterRegexNode(Current.Value!.Value);
                Advance();
                return charNode;
            case RegexTokenType.Any:
                var anyNode = new AnyCharacterRegexNode();
                Advance();
                return anyNode;
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
        bool negate = false;
        if (Current.Type == RegexTokenType.Negation)
        {
            negate = true;
            Advance();
        }
        var node = new CharacterSetRegexNode(negate);

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
