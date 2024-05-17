using Core.NFA.Algorithms;
using Core.RegularExpressions;

namespace UnitTests;

public class MinimizeDFATests
{
    [Fact]
    public void MinimizeDFAExample1()
    {
        var regex = new Regex("(a|b)*abb");
        var nfa = regex.ConvertToNFA();
        var sc = new SubsetConstruction();
        var node = sc.Execute(nfa);

        var minimizer = new StateMinimization();
        minimizer.Execute(node);
    }
}
