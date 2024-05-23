using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("[Start {Start.Id}], [{End.Count} end states]")]
public class Graph
{
    public Node Start = null!;
    public List<Node> End = [];

    public static Graph Combine(Graph g1, Graph g2)
    {
        var result = new Graph() { Start = new Node() };

        result.Start.AddEpsilonTransition(g1.Start);
        result.Start.AddEpsilonTransition(g2.Start);

        result.End.AddRange(g1.End);
        result.End.AddRange(g2.End);

        return result;
    }
}
