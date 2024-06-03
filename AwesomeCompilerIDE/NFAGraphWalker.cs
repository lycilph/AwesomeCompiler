using AwesomeCompilerCore.Graphs;

namespace AwesomeCompilerIDE;

public class NFAGraphWalker
{
    public Microsoft.Msagl.Drawing.Graph Graph { get; private set; } = new();

    public void WalkGraph(GraphNode node)
    {
        var visited = new HashSet<GraphNode>();
        var toVisit = new Stack<GraphNode>();

        toVisit.Push(node);

        Graph.Attr.LayerDirection = Microsoft.Msagl.Drawing.LayerDirection.LR;
        Graph.Attr.SimpleStretch = true;

        while (toVisit.Count > 0)
        {
            var n = toVisit.Pop();
            if (visited.Contains(n))
                continue;

            visited.Add(n);

            var graphNode = Graph.AddNode(n.Id.ToString());
            if (n.IsFinal)
            { 
                graphNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Octagon;
                graphNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Gray;
            }

            foreach (var t in n.Transitions)
            {
                Graph.AddEdge(n.Id.ToString(), t.Symbol.ToString(), t.To.Id.ToString());

                if (!visited.Contains(t.To))
                    toVisit.Push(t.To);
            }
        }
    }
}
