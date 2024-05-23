using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using Core.RegularExpressions.Nodes;

namespace AwesomeCompilerTests.Core.RegularExpressions;

public class SimplifyTests
{
    [Fact]
    public void SimplifyAlternationTest1()
    {
        // Arranger
        var input = "[a-z]|.";
        var regex = new Regex(input);

        // Act
        var visitor = new SimplifyVisitor();
        regex.Node.Accept(visitor);

        // Assert
        Assert.True(regex.Node is AnyCharacterNode);
    }

    [Fact]
    public void SimplifyAlternationTest2()
    {
        // Arranger
        var input = "a|.";
        var regex = new Regex(input);

        // Act
        var visitor = new SimplifyVisitor();
        regex.Node.Accept(visitor);

        // Assert
        Assert.True(regex.Node is AnyCharacterNode);
    }

    [Fact]
    public void SimplifyAlternationTest3()
    {
        // Arranger
        var regex = new Regex("a");
        var inputNode = new CharacterSetNode('a');

        // Act
        var visitor = new SimplifyVisitor();
        regex.Node.Accept(visitor);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }

    [Fact]
    public void SimplifyAlternationTest4()
    {
        // Arranger
        var regex = new Regex("a|b");
        var inputNode = new CharacterSetNode('a');
        inputNode.Add('b');

        // Act
        var visitor = new SimplifyVisitor();
        regex.Node.Accept(visitor);

        // Assert
        Assert.Equal(inputNode, regex.Node, new RegexNodeComparerIgnoreId());
    }
}
