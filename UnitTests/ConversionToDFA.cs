using Core.NFA.Algorithms;
using Core.RegularExpressions;

namespace UnitTests;

public class ConversionToDFA
{
    [Fact]
    public void Example1()
    {
        var regex = new Regex("(a|b)*abb");
        var nfa = regex.ConvertToNFA();

        var sc = new SubsetConstruction();
        var node = sc.Execute(nfa);

        Assert.NotNull(node);
    }

    [Fact]
    public void Example2()
    {
        var regex = new Regex("[a-z]([a-z0-9_])*");
        var nfa = regex.ConvertToNFA();

        var sc = new SubsetConstruction();
        var node = sc.Execute(nfa);

        Assert.NotNull(node);
    }
}
