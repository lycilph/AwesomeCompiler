using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Misc;
using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerUnitTests;

public class RegexParserTests
{
    [Fact]
    public void ParseRegex1()
    {
        // Arrange
        var input = "(a|b)*abb";
        var inputNode =
            new Regex("(a|b)*abb",
                new ConcatenationRegexNode(
                    new ConcatenationRegexNode(
                        new ConcatenationRegexNode(
                            new StarRegexNode(
                                new AlternationRegexNode(
                                    new CharacterRegexNode('a'),
                                    new CharacterRegexNode('b'))),
                            new CharacterRegexNode('a')),
                        new CharacterRegexNode('b')),
                    new CharacterRegexNode('b')));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex, new IgnoreIdRegexNodeComparer());
    }
}
