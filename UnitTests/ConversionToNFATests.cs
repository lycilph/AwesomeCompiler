using Core.RegularExpressions;

namespace UnitTests;

public class ConversionToNFATests
{
    [Fact]
    public void ConvertCharacterSetToGraph()
    {
        var set = new CharacterSetNode("0-9");
        var graph = set.ConvertToNFA();

        var str = graph.GenerateDotGraph();

        Assert.NotNull(graph);
    }
}
