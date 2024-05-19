using Core.RegularExpressions;

namespace AwesomeCompilerTests.Core.RegularExpressions;

// All classes marked with the same collection are run sequentially
// Currently needed, as nodes use a static counter to generate ids
[Collection("Must run sequentially #1")]
public class NodesTests
{
    public NodesTests()
    {
        RegexNode.ResetCounter();
    }

    [Fact]
    public void CharacterNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterNode('a');

        RegexNode.ResetCounter();
        var n2 = new CharacterNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2);
    }

    [Fact]
    public void CharacterSetNodeEqualityTest()
    {
        // Arrange
        var n1 = new CharacterSetNode(false);
        n1.Add('a', 'z');
        n1.Add('A', 'Z');

        RegexNode.ResetCounter();
        var n2 = new CharacterSetNode(false);
        n2.Add('a', 'z');
        n2.Add('A', 'Z');

        // Act

        // Assert
        Assert.Equal(n1, n2);
    }

    [Fact]
    public void AlternationNodeEqualityTest()
    {
        // Arrange
        var n1 = new AlternationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        RegexNode.ResetCounter();
        var n2 = new AlternationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        // Act
        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        // Assert
        Assert.Equal(n1, n2);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void ConcatenationNodeEqualityTest()
    {
        // Arrange
        var n1 = new ConcatenationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        RegexNode.ResetCounter();
        var n2 = new ConcatenationNode(
            new CharacterNode('a'),
            new CharacterNode('b'));

        // Act
        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        // Assert
        Assert.Equal(n1, n2);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void StarNodeEqualityTest()
    {
        // Arrange
        var n1 = new StarNode(
            new CharacterNode('a'));

        RegexNode.ResetCounter();
        var n2 = new StarNode(
            new CharacterNode('a'));

        // Act
        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        // Assert
        Assert.Equal(n1, n2);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void PlusNodeEqualityTest()
    {
        // Arrange
        var n1 = new PlusNode(
            new CharacterNode('a'));

        RegexNode.ResetCounter();
        var n2 = new PlusNode(
            new CharacterNode('a'));

        // Act
        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        // Assert
        Assert.Equal(n1, n2);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void OptionalNodeEqualityTest()
    {
        // Arrange
        var n1 = new OptionalNode(
            new CharacterNode('a'));

        RegexNode.ResetCounter();
        var n2 = new OptionalNode(
            new CharacterNode('a'));

        // Act
        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        // Assert
        Assert.Equal(n1, n2);
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void RegexNodeEqualityTest()
    {
        // Arrange
        RegexNode n1 = new CharacterNode('a');

        RegexNode.ResetCounter();
        RegexNode n2 = new CharacterNode('a');

        // Act

        // Assert
        Assert.Equal(n1, n2);
    }
}
