using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Misc;
using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerUnitTests;

public class RegexNodeEqualityTests
{
    [Fact]
    public void RegexEqualityTest()
    {
        // Arrange
        var n1 = new Regex("a");
        var n2 = new Regex("a");

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void AnyCharacterNodeEqualityTest()
    {
        // Arrange
        var n1 = new AnyCharacterRegexNode();
        var n2 = new AnyCharacterRegexNode();

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void CharacterNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterRegexNode('a');
        var n2 = new CharacterRegexNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void CharacterSetNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterSetRegexNode(false);
        n1.Add('a', 'z');
        n1.Add('A', 'Z');

        var n2 = new CharacterSetRegexNode(false);
        n2.Add('a', 'z');
        n2.Add('A', 'Z');

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void AlternationNodeEqualityTest()
    {
        // Arrange
        var n1 = new AlternationRegexNode(
            new CharacterRegexNode('a'),
            new CharacterRegexNode('b'));

        var n2 = new AlternationRegexNode(
            new CharacterRegexNode('a'),
            new CharacterRegexNode('b'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void ConcatenationNodeEqualityTest()
    {
        // Arrange
        var n1 = new ConcatenationRegexNode(
            new CharacterRegexNode('a'),
            new CharacterRegexNode('b'));

        var n2 = new ConcatenationRegexNode(
            new CharacterRegexNode('a'),
            new CharacterRegexNode('b'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void StarNodeEqualityTest()
    {
        // Arrange
        var n1 = new StarRegexNode(
            new CharacterRegexNode('a'));

        var n2 = new StarRegexNode(
            new CharacterRegexNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void PlusNodeEqualityTest()
    {
        // Arrange
        var n1 = new PlusRegexNode(
            new CharacterRegexNode('a'));

        var n2 = new PlusRegexNode(
            new CharacterRegexNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void OptionalNodeEqualityTest()
    {
        // Arrange
        var n1 = new OptionalRegexNode(
            new CharacterRegexNode('a'));

        var n2 = new OptionalRegexNode(
            new CharacterRegexNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }

    [Fact]
    public void RegexNodeEqualityTest()
    {
        // Arrange
        RegexNode n1 = new CharacterRegexNode('a');
        RegexNode n2 = new CharacterRegexNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2, new IgnoreIdRegexNodeComparer());
    }
}
