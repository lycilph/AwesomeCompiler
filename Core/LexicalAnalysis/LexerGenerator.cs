using Core.Common;
using Core.Graphs;
using Core.Graphs.Algorithms;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace Core.LexicalAnalysis;

public class LexerGenerator<T>
{
    private readonly List<Rule<T>> rules = [];

    public void Add(Rule<T> rule)
    {
        rules.Add(rule);
    }

    public void Generate()
    {
        Directory.CreateDirectory("Output");

        GenerateGraphs();
    }

    private void GenerateGraphs()
    {
        var nfas = ConvertRulesToNFAs();

        var combined_nfa = Graph.Combine(nfas);
        RenderDotGraph(@"Output\combined_nfa.png", combined_nfa.Start);

        var dfa = NFAToDFACreator.Run(combined_nfa.Start);
        RenderDotGraph(@"Output\dfa.png", dfa);

        var minimized_dfa = DFAStateMinimizer.Run(dfa);
        RenderDotGraph(@"Output\minimized_dfa.png", minimized_dfa);
    }

    private IEnumerable<Graph> ConvertRulesToNFAs()
    {
        var result = new List<Graph>();

        var simplifier = new SimplifyVisitor();
        var nfa_visitor = new RegexToNFAVisitor();
        foreach (var rule in rules)
        {
            rule.Regex.Node.Accept(simplifier);
            RenderDotGraph(@"Output\" + rule.Type!.ToString() + "_regex.png", rule.Regex);

            var nfa = rule.Regex.Node.Accept(nfa_visitor);
            nfa.End.First().Rule = rule;
            nfa.End.First().Skip = skip;
            RenderDotGraph(@"Output\" + rule + "_nfa.png", nfa.Start);

            result.Add(nfa);
        }

        return result;
    }

    private static void RenderDotGraph(string filename, Node n)
    {
        var graph = DotGraphNodeWalker.Generate(n);
        DotWrapper.Render(filename, graph);
    }

    private static void RenderDotGraph(string filename, Regex re)
    {
        var graph = DotGraphVisitor.Generate(re);
        DotWrapper.Render(filename, graph);
    }
}
