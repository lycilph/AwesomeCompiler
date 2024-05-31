using AwesomeCompilerCore.RegularExpressions;

namespace AwesomeCompilerUnitTests;

public class RegexTokenizerTests
{
    [Fact]
    public void RecognizeAllTokens()
    {
        // Arrange
        var input = @"()[]-^|*+?a.\.";
        var input_tokens = new List<RegexToken>
        {
            new(RegexTokenType.LeftParenthesis),
            new(RegexTokenType.RightParenthesis),
            new(RegexTokenType.LeftBracket),
            new(RegexTokenType.RightBracket),
            new(RegexTokenType.Hyphen),
            new(RegexTokenType.Negation),
            new(RegexTokenType.Alternation),
            new(RegexTokenType.Star),
            new(RegexTokenType.Plus),
            new(RegexTokenType.Optional),
            new(RegexTokenType.Character, 'a'),
            new(RegexTokenType.Any),
            new(RegexTokenType.Character, '.'),
            new(RegexTokenType.EndOfInput)
        };

        // Act
        var tokens = RegexTokenizer.Tokenize(input);

        // Assert
        Assert.Equal(input_tokens, tokens);
    }

    [Fact]
    public void RecognizeEscapeCharacters1()
    {
        // Arrange
        var input = @"r\rt\tn\n";
        var input_tokens = new List<RegexToken>
        {
            new(RegexTokenType.Character, 'r'),
            new(RegexTokenType.Character, '\r'),
            new(RegexTokenType.Character, 't'),
            new(RegexTokenType.Character, '\t'),
            new(RegexTokenType.Character, 'n'),
            new(RegexTokenType.Character, '\n'),
            new(RegexTokenType.EndOfInput)
        };

        // Act
        var tokens = RegexTokenizer.Tokenize(input);

        // Assert
        Assert.Equal(input_tokens, tokens);
    }

    [Fact]
    public void RecognizeEscapeCharacters2()
    {
        // Arrange
        var input = "\"\\\\";
        var input_tokens = new List<RegexToken>
        {
            new(RegexTokenType.Character, '"'),
            new(RegexTokenType.Character, '\\'),
            new(RegexTokenType.EndOfInput)
        };

        // Act
        var tokens = RegexTokenizer.Tokenize(input);

        // Assert
        Assert.Equal(input_tokens, tokens);
    }
}