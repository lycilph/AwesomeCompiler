using Core.RegularExpressions;

namespace UnitTests;

public class ParseCharacterSets
{
    [Fact]
    public void ParseCharacterSets1()
    {
        var pattern = @"[0-9]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new CharacterSetNode("0-9"));
        
        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseCharacterSets2()
    {
        var pattern = @"[^0-9]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new CharacterSetNode("0-9", true));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseCharacterSets3()
    {
        var pattern = @"[abc]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        seq.Add(new CharacterSetNode("a-c"));

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseCharacterSets4()
    {
        var pattern = @"[a-zA-Z_]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        var set = new CharacterSetNode();
        set.AddRange('a', 'z');
        set.AddRange('A', 'Z');
        set.AddCharacter('_');
        seq.Add(set);

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }

    [Fact]
    public void ParseCharacterSets5()
    {
        var pattern = @"[a-z_123+7-9]";
        var regex = new Regex(pattern);

        // Create tree manually here
        var seq = new SequenceNode();
        var set = new CharacterSetNode();
        set.AddRange('a', 'z');
        set.AddRange('1', '3');
        set.AddRange('7', '9');
        set.AddCharacter('_');
        set.AddCharacter('+');
        seq.Add(set);

        // Compare trees
        Assert.Equal(regex.GetRoot(), seq);
    }
}
