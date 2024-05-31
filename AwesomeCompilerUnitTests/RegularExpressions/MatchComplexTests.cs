using AwesomeCompilerCore.RegularExpressions;

namespace AwesomeCompilerUnitTests.RegularExpressions;

public class MatchComplexTests
{
    [Fact]
    public void MatchComplexInput1()
    {
        var regex = new Regex("[0-9]+(.[0-9]*)?");

        Assert.True(regex.Match("1245.988"));
        Assert.True(regex.Match("1245."));
        Assert.True(regex.Match("1245"));
        Assert.False(regex.Match(".1245"));
    }

    [Fact]
    public void MatchComplexInput2()
    {
        var regex = new Regex("[a-zA-Z_][a-zA-Z_0-9]*");

        Assert.True(regex.Match("test"));
        Assert.False(regex.Match("0test8"));
    }
}
