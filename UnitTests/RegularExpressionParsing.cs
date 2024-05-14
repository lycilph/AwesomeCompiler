using Core.RegularExpressions;

namespace UnitTests;

public class RegularExpressionParsing
{
    [Fact]
    public void ParseComplexExpression1()
    {
        var pattern = @"[0-9]+(.[0-9]*)?";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new PlusNode(new CharacterSetNode("0-9")));

        var seq2 = new SequenceNode();
        seq2.Add(new MatchAnyCharacterNode());
        seq2.Add(new StarNode(new CharacterSetNode("0-9")));

        seq.Add(new OptionalNode(new GroupNode(seq2)));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseComplexExpression2()
    {
        var pattern = @"([0-9]|[a-z])*";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();

        var seq2 = new SequenceNode();
        seq2.Add(new AlternationNode()
        {
            Left = new CharacterSetNode("0-9"),
            Right = new CharacterSetNode("a-z")
        });

        seq.Add(new StarNode(new GroupNode(seq2)));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseComplexExpression3()
    {
        var pattern = @"[0-9]|[a-z]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new AlternationNode()
        {
            Left = new CharacterSetNode("0-9"),
            Right = new CharacterSetNode("a-z")
        });

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseComplexExpression4()
    {
        var pattern = @"abc|d";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new MatchSingleCharacterNode('a'));
        seq.Add(new MatchSingleCharacterNode('b'));
        seq.Add(new AlternationNode()
        {
            Left = new MatchSingleCharacterNode('c'),
            Right = new MatchSingleCharacterNode('d')
        }); ;

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }
}