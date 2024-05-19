using Core.RegularExpressions;
using Core.RegularExpressions.Nodes;

namespace AwesomeCompilerTests.Core.RegularExpressions;

public class ParserTests
{
    [Fact]
    public void ParseRegex1()
    {
        // Arrange
        var input = "(a|b)*abb";
        var inputNode =
            new ConcatenationNode(
                new ConcatenationNode(
                    new ConcatenationNode(
                        new StarNode(
                            new AlternationNode(
                                new CharacterNode('a'),
                                new CharacterNode('b'))),
                        new CharacterNode('a')),
                    new CharacterNode('b')),
                new CharacterNode('b'));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void ParseRegex2()
    {
        // Arrange
        var input = "(a|b)*ab+b?";
        var inputNode =
            new ConcatenationNode(
                new ConcatenationNode(
                    new ConcatenationNode(
                        new StarNode(
                            new AlternationNode(
                                new CharacterNode('a'),
                                new CharacterNode('b'))),
                        new CharacterNode('a')),
                    new PlusNode(
                        new CharacterNode('b'))),
                new OptionalNode(
                    new CharacterNode('b')));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void ParseRegex3()
    {
        // Arrange
        var input = "[a-z][a-z0-9]*";
        var set1 = new CharacterSetNode(false);
        set1.Add('a', 'z');
        var set2 = new CharacterSetNode(false);
        set2.Add('a', 'z');
        set2.Add('0', '9');
        var inputNode =
            new ConcatenationNode(
                set1,
                new StarNode(
                    set2));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void ParseRegex4()
    {
        // Arrange
        var input = @"[0-9](\.[0-9]+)?";
        var inputNode =
            new ConcatenationNode(
                new CharacterSetNode('0','9'),
                new OptionalNode(
                    new ConcatenationNode(
                        new CharacterNode('.'),
                        new PlusNode(
                            new CharacterSetNode('0','9')))));

        // Act
        var regex = new Regex(input);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void FailOnUnexpectedToken()
    {
        // Arrange
        var input = @"a]";

        // Act / Assert
        Assert.Throws<InvalidDataException>(() => new Regex(input),);
    }
}
