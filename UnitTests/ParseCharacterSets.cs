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

        // Check that character set is correct

        Assert.True(false);
    }

    [Fact]
    public void ParseCharacterSets3()
    {
        var pattern = @"[abc]";
        var regex = new Regex(pattern);

        // Check that character set is correct

        Assert.True(false);
    }

    [Fact]
    public void ParseCharacterSets4()
    {
        var pattern = @"[a-zA-Z_]";
        var regex = new Regex(pattern);

        // Check that character set is correct

        Assert.True(false);
    }

    [Fact]
    public void ParseCharacterSets5()
    {
        var pattern = @"[a-zA-Z_1234-9]";
        var regex = new Regex(pattern);

        // Check that character set is correct

        Assert.True(false);
    }
}
