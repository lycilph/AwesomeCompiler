using AwesomeCompilerCore.RegularExpressions;

namespace AwesomeCompilerUnitTests.RegularExpressions;

public class MatchSimpleTests
{
    [Fact]
    public void MatchSimpleInput1()
    {
        // Arrange
        var regex = new Regex(@"abc|d");

        // Act

        // Assert
        Assert.True(regex.Match("abc"));
        Assert.True(regex.Match("abd"));
        Assert.False(regex.Match("abq"));
    }

    [Fact]
    public void MatchSimpleInput2()
    {
        // Arrange
        var regex = new Regex(@"abc|d");

        // Act

        // Assert
        Assert.False(regex.Match("ab"));
    }

    [Fact]
    public void MatchSimpleInput3()
    {
        // Arrange
        var regex = new Regex(@"abc|d");

        // Act

        // Assert
        Assert.True(regex.Match("abcc"));
    }

    [Fact]
    public void MatchNegativeInput1()
    {
        // Arrange
        var regex = new Regex(@"ab[^c]");

        // Act

        // Assert
        Assert.False(regex.Match("abc"));
        Assert.True(regex.Match("abd"));
    }

    [Fact]
    public void MatchNegativeInput2()
    {
        // Arrange
        var regex = new Regex(@"[0-9][^a-z]");

        // Act

        // Assert
        Assert.True(regex.Match("42"));
        Assert.False(regex.Match("0d"));
    }
}
