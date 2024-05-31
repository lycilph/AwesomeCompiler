using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Misc;
using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerUnitTests.RegularExpressions;

public class ParserTests
{
    [Fact]
    public void ParseRegex1()
    {
        // Arrange
        var input = "(a|b)*abb";
        var inputNode =
            new Regex(input,
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

    [Fact]
    public void ParseRegex2()
    {
        // Arrange
        var input = "(a|b)*ab+b?";
        var inputNode =
            new Regex(input,
                new ConcatenationRegexNode(
                    new ConcatenationRegexNode(
                        new ConcatenationRegexNode(
                            new StarRegexNode(
                                new AlternationRegexNode(
                                    new CharacterRegexNode('a'),
                                    new CharacterRegexNode('b'))),
                            new CharacterRegexNode('a')),
                        new PlusRegexNode(
                            new CharacterRegexNode('b'))),
                    new OptionalRegexNode(
                        new CharacterRegexNode('b'))));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void ParseRegex3()
    {
        // Arrange
        var input = "[a-z][a-z0-9]*";
        var set1 = new CharacterSetRegexNode(false);
        set1.Add('a', 'z');
        var set2 = new CharacterSetRegexNode(false);
        set2.Add('a', 'z');
        set2.Add('0', '9');
        var inputNode =
            new Regex(input,
                new ConcatenationRegexNode(
                    set1,
                    new StarRegexNode(
                        set2)));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void ParseRegex4()
    {
        // Arrange
        var input = @"[0-9](\.[0-9]+)?";
        var inputNode =
            new Regex(input,
                new ConcatenationRegexNode(
                    new CharacterSetRegexNode('0', '9'),
                    new OptionalRegexNode(
                        new ConcatenationRegexNode(
                            new CharacterRegexNode('.'),
                            new PlusRegexNode(
                                new CharacterSetRegexNode('0', '9'))))));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void FailOnUnexpectedToken()
    {
        // Arrange
        var input = @"a]";

        // Act / Assert
        Assert.Throws<InvalidDataException>(() => new Regex(input));
    }
}
