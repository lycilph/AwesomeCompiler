using Core.RegularExpressions.Nodes;

namespace AwesomeCompilerTests.Core.RegularExpressions;

public class NodesTests
{
    [Fact]
    public void AnyCharacterNodeEqualityTest()
    {
        // Arrange
        var n1 = new AnyCharacterNode();
        var n2 = new AnyCharacterNode();

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void CharacterNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterNode('a');
        var n2 = new CharacterNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void CharacterSetNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterSetNode(false);
        n1.Add('a', 'z');
        n1.Add('A', 'Z');

        var n2 = new CharacterSetNode(false);
        n2.Add('a', 'z');
        n2.Add('A', 'Z');

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void AlternationNodeEqualityTest()
    {
        // Arrange
        var n1 = new AlternationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        var n2 = new AlternationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void ConcatenationNodeEqualityTest()
    {
        // Arrange
        var n1 = new ConcatenationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        var n2 = new ConcatenationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void StarNodeEqualityTest()
    {
        // Arrange
        var n1 = new StarNode(
            new CharacterNode('a'));

        var n2 = new StarNode(
            new CharacterNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void PlusNodeEqualityTest()
    {
        // Arrange
        var n1 = new PlusNode(
            new CharacterNode('a'));

        var n2 = new PlusNode(
            new CharacterNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void OptionalNodeEqualityTest()
    {
        // Arrange
        var n1 = new OptionalNode(
            new CharacterNode('a'));

        var n2 = new OptionalNode(
            new CharacterNode('a'));

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void RegexNodeEqualityTest()
    {
        // Arrange
        RegexNode n1 = new CharacterNode('a');
        RegexNode n2 = new CharacterNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2, new RegexNodeComparerIgnoreId());
    }
}
