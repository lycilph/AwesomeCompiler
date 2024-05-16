using System.Text;

namespace Core.NFA;

public class Graph
{
    public Node Start { get; set; } = null!;
    public Node End { get; set; } = null!;

    public string GenerateDotGraph()
    {
        var sb = new StringBuilder();
        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        sb.AppendLine("digraph {");
        sb.AppendLine("rankdir=LR;");

        toVisit.Push(Start);
        while (toVisit.Count > 0)
        {
            var node = toVisit.Pop();

            if (!visited.Contains(node))
            {
                visited.Add(node);

                if (node.IsFinal)
                    sb.AppendLine($"{node.Id} [shape=doublecircle]");
                else
                    sb.AppendLine($"{node.Id} [shape=circle]");

                foreach (var t in node.Transitions)
                {
                    string? label;
                    if (t.Epsilon)
                        label = "Epsilon";
                    else if (t.MatchAny)
                        label = "Any";
                    else
                        label = t.Label;
                    sb.AppendLine($"{t.From.Id} -> {t.To.Id} [label=\"{label}\"]");
                    toVisit.Push(t.To);
                }
            }
        }
        
        sb.AppendLine("}");

        return sb.ToString();
    }
}
