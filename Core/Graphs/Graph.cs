using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("[Start {Start.Id}], [{End.Count} end states]")]
public class Graph
{
    public Node Start = null!;
    public List<Node> End = [];
}
