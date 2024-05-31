using AwesomeCompilerCore.Common;

namespace AwesomeCompilerUnitTests;

public class CharSetTests
{
    [Fact]
    public void CharSetLabel()
    {
        // Arrange
        var cs = new CharSet('a','z');

        // Act
        var str = cs.ToString();

        // Assert
        Assert.Equal("[a-z]", str);
    }

    [Fact]
    public void AllCharSet()
    {
        // Arrange
        var cs = CharSet.All();

        // Act

        // Assert
        Assert.Equal(128, cs.Get().Count);
    }

    [Fact]
    public void NegativeCharSet()
    {
        // Arrange
        var cs = new CharSet('a', 'z') { IsNegative = true };

        // Act
        var set = cs.Get();

        // Assert
        Assert.Equal(102, set.Count);
    }
}
