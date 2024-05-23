﻿using Core.Common;
using Core.Graphs;
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
            var numberRegex = new Regex(@"[0-9](\.[0-9]+)?");
            RenderDotGraph("number_regex.png", numberRegex);

            var identifierRegex = new Regex("[a-z_]([a-z_]|[A-Z]|[0-9])*");
            RenderDotGraph("identifier_regex.png", identifierRegex);

            var numberNFA = RegexToNFAVisitor.Accept(numberRegex);
            RenderDotGraph("number_nfa.png", numberNFA.Start);

            var identifierNFA = RegexToNFAVisitor.Accept(identifierRegex);
            RenderDotGraph("identifier_nfa.png", identifierNFA.Start);

            var nfa = Graph.Combine(numberNFA, identifierNFA);
            RenderDotGraph("combined_nfa.png", nfa.Start);

            var dfa = NFAToDFACreator.Run(nfa.Start);
            RenderDotGraph("dfa.png", dfa);

            var minimized = DFAStateMinimizer.Run(dfa);
            RenderDotGraph("minimized_dfa.png", minimized);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Console.Write("Press any key to continue...");
        //Console.ReadKey();
    }

    private static void RenderDotGraph(string filename, Regex re)
    {
        var graph = DotGraphVisitor.Generate(re);
        DotWrapper.Render(filename, graph);
    }

    private static void RenderDotGraph(string filename, Node n)
    {
        var graph = DotGraphNodeWalker.Generate(n);
        DotWrapper.Render(filename, graph);
    }

    private static void Test1()
    {
        var str = @"[a-z_]([a-z_]|[A-Z]|[0-9])*";
        //var str = @"(a|b)*abb";
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

        var dfaStateMinizser = new DFAStateMinimizer();
        var minimizedDFA = dfaStateMinizser.Execute(dfa);

        var minimizedDfaDotGraph = DotGraphNodeWalker.Generate(minimizedDFA);
        DotWrapper.Render("minimized_dfa.png", minimizedDfaDotGraph);
    }
}
