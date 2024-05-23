namespace Core.Graphs.Algorithms;

public static class Collector
{
    public static HashSet<Symbol> CollectSymbols(Node node)
    {
        HashSet<Symbol> symbols = [];

        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        // Initialize stack
        toVisit.Push(node);

        while (toVisit.Count > 0)
        {
            var n = toVisit.Pop();
            visited.Add(n);

            foreach (var t in n.Transitions)
            {
                if (!t.Symbol.IsEpsilon)
                    symbols.Add(t.Symbol);

                if (!visited.Contains(t.To))
                    toVisit.Push(t.To);
            }
        }

        return symbols;
    }

    public static HashSet<Node> CollectNodes(Node start)
    {
        var visited = new HashSet<Node>();
        var toVisit = new Queue<Node>();

        // Initialize queue
        toVisit.Enqueue(start);

        while (toVisit.Count > 0)
        {
            var node = toVisit.Dequeue();
            visited.Add(node);

            foreach (var t in node.Transitions)
                if (!visited.Contains(t.To))
                    toVisit.Enqueue(t.To);
        }

        return visited;
    }
}
