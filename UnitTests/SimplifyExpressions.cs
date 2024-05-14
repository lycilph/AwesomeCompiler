using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace UnitTests;

public class SimplifyExpressions
{
    [Fact]
    public void SimplifyExpressions1()
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
    public void SimplifyExpressions2()
    {
        var regex = new Regex("[0-9]|[a-z]");
        var visitor = new SimplifyVisitor();

        regex.GetRoot().Accept(visitor);
    }
}
