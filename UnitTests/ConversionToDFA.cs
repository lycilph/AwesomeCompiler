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
        var dfa = sc.Execute(nfa);
    }
}
