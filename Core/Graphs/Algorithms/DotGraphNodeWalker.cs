using System.Text;

namespace Core.Graphs.Algorithms;

public class DotGraphNodeWalker
{
    private readonly StringBuilder sb = new();

    public static string Generate(Node node)
    {
        var walker = new DotGraphNodeWalker();
        walker.Begin();

        walker.WalkTree(node);

        walker.End();
        return walker.sb.ToString();
    }

    public void Begin()
    {
        sb.AppendLine("digraph {");
        sb.AppendLine("  rankdir = \"LR\"");
    }

    public void End()
    {
        sb.AppendLine("}");
    }

    public void WalkTree(Node node)
    {
        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        toVisit.Push(node);

        while (toVisit.Count > 0)
        {
            var n = toVisit.Pop();
            if (visited.Contains(n))
                continue;

            visited.Add(n);

            var shape = n.IsFinal ? "doublecircle" : "circle";
            sb.AppendLine($"  {n.Id} [shape={shape},label=\"{n.Id}\"]");

            foreach (var t in n.Transitions)
            {
                sb.AppendLine($"  {n.Id} -> {t.To.Id} [label=\"{t.Symbol}\"]");
                if (!visited.Contains(t.To))
                    toVisit.Push(t.To);
            }
        }
    }
}