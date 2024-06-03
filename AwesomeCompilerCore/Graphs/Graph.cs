using System.Diagnostics;

namespace AwesomeCompilerCore.Graphs;

[DebuggerDisplay("[Start {Start.Id}], [{End.Count} end states]")]
public class Graph
{
    public GraphNode Start = null!;
    public List<GraphNode> End = [];

    public static Graph Combine(IEnumerable<Graph> graphs)
    {
        var result = new Graph() { Start = new GraphNode() };

        foreach (var g in graphs)
        {
            result.Start.AddEpsilonTransition(g.Start);
            result.End.AddRange(g.End);
        }

        return result;
    }
}