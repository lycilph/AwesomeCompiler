using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace UnitTests;

public class SimplifyExpressions
{
    [Fact]
    public void SimplifySequenceExpressions()
    {
        var regex = new Regex("([0-9])");
        var visitor = new SimplifyVisitor();

        regex.GetRoot().Accept(visitor);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new CharacterSetNode("0-9"));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void SimplifyGroupExpressions()
    {
        var regex = new Regex("[0-9]+(.[0-9]+)?");
        var visitor = new SimplifyVisitor();

        regex.GetRoot().Accept(visitor);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new PlusNode(new CharacterSetNode("0-9")));

        var seq2 = new SequenceNode();
        seq2.Add(new MatchAnyCharacterNode());
        seq2.Add(new PlusNode(new CharacterSetNode("0-9")));

        seq.Add(new OptionalNode(seq2));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void SimplifyAlternationExpressions1()
    {
        var regex = new Regex("[0-9]|[a-z]");
        var visitor = new SimplifyVisitor();

        regex.GetRoot().Accept(visitor);

        // Create tree manually here
        var seq = new SequenceNode();
        var set = new CharacterSetNode("0-9");
        set.AddRange('a','z');
        seq.Add(set);

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }


    [Fact]
    public void SimplifyAlternationExpressions2()
    {
        var regex = new Regex("a|(b)");
        var visitor = new SimplifyVisitor();

        regex.GetRoot().Accept(visitor);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new CharacterSetNode('a','b'));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }
}
