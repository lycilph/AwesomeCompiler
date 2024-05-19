using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace AwesomeCompilerTests.Core.RegularExpressions;

// All classes marked with the same collection are run sequentially
// Currently needed, as nodes use a static counter to generate ids
[Collection("Must run sequentially #1")]
public class SimplifyTests
{
    [Fact]
    public void SimplifyAlternationTest1()
    {
        // Arranger
        var input = "[a-z]|.]";
        var regex = new Regex(input);

        // Act
        var visitor = new SimplifyVisitor();
        regex.Node.Accept(visitor);

        // Assert
        Assert.True(regex.Node is AnyCharacterNode);
    }
}
