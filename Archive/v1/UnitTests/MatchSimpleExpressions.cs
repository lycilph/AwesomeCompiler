using Core.RegularExpressions;

namespace UnitTests;

public class MatchSimpleExpressions
{
    [Fact]
    public void MatchSimpleInput1()
    {
        var pattern = @"abc|d";
        var regex = new Regex(pattern);

        var input1 = "abc";
        var input2 = "abd";
        var input3 = "abq";

        Assert.True(regex.IsMatch(input1));
        Assert.True(regex.IsMatch(input2));
        Assert.False(regex.IsMatch(input3));
    }

    [Fact]
    public void MatchSimpleInput2()
    {
        var pattern = @"abc|d";
        var regex = new Regex(pattern);

        var input1 = "ab";

        Assert.False(regex.IsMatch(input1));
    }

    [Fact]
    public void MatchSimpleInput3()
    {
        var pattern = @"abc|d";
        var regex = new Regex(pattern);

        var input1 = "abcc";

        Assert.True(regex.IsMatch(input1));
    }

    [Fact]
    public void MatchNegativeInput1()
    {
        var pattern = @"ab[^c]";
        var regex = new Regex(pattern);

        Assert.False(regex.IsMatch("abc"));
        Assert.True(regex.IsMatch("abd"));
    }

    [Fact]
    public void MatchNegativeInput2()
    {
        var pattern = @"[0-9][^a-z]";
        var regex = new Regex(pattern);

        Assert.True(regex.IsMatch("42"));
        Assert.False(regex.IsMatch("0d"));
    }
}
