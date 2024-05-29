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
