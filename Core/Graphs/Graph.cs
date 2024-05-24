using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("[Start {Start.Id}], [{End.Count} end states]")]
public class Graph
{
    public Node Start = null!;
    public List<Node> End = [];

    public static Graph Combine(IEnumerable<Graph> graphs)
    {
        var result = new Graph() { Start = new Node() };

        foreach (var g in graphs)
        {
            result.Start.AddEpsilonTransition(g.Start);
            result.End.AddRange(g.End);
        }

        return result;
    }
}
