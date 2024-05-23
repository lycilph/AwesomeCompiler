using Core.Common;
using Core.Graphs.Algorithms;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace AwesomeCompilerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var str = @"(a|b)*abb";
            var regex = new Regex(str);

            Console.WriteLine($"Input {str}");
            Console.WriteLine();

            var regexDotGraph = DotGraphVisitor.Generate(regex);
            DotWrapper.Render("regex.png", regexDotGraph);

            var nfa = RegexToNFAVisitor.Accept(regex);

            var nfaDotGraph = DotGraphNodeWalker.Generate(nfa.Start);
            DotWrapper.Render("nfa.png", nfaDotGraph);

            var dfaCreator = new NFAToDFACreator();
            var dfa = dfaCreator.Execute(nfa.Start);

            var dfaDotGraph = DotGraphNodeWalker.Generate(dfa);
            DotWrapper.Render("dfa.png", dfaDotGraph);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Console.Write("Press any key to continue...");
        //Console.ReadKey();
    }
}
