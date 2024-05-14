using Core.RegularExpressions;

namespace UnitTests;

public class MatchComplexExpressions
{
    [Fact]
    public void MatchComplexInput1()
    {
        var regex = new Regex("[0-9]+(.[0-9]*)?");

        Assert.True(regex.IsMatch("1245.988"));
        Assert.True(regex.IsMatch("1245."));
        Assert.True(regex.IsMatch("1245"));
        Assert.False(regex.IsMatch(".1245"));
    }

    [Fact]
    public void MatchComplexInput2()
    {
        var regex = new Regex("[a-zA-Z_][a-zA-Z_0-9]*");

        Assert.True(regex.IsMatch("test"));
        Assert.False(regex.IsMatch("0test8"));
    }
}
