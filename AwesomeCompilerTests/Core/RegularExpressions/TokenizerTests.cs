using Core.RegularExpressions;

namespace AwesomeCompilerTests.Core.RegularExpressions;

public class TokenizerTests
{
    [Fact]
    public void RecognizeAllTokens()
    {
        // Arrange
        var input = "()[]-^|*+?a";
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
            new(RegexTokenType.EndOfInput)
        };

        // Act
        var tokenizer = new RegexTokenizer(input);
        var tokens = tokenizer.Tokenize();

        // Assert
        Assert.Equal(input_tokens, tokens);
    }
}
