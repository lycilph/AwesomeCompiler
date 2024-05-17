namespace Core.NFA.Algorithms;

public static class SymbolCollector
{
    public static HashSet<Symbol> Collect(Node node)
    {
        var symbols = new HashSet<Symbol>(new SymbolComparer());
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
                if (!t.Symbol.isEpsilon)
                    symbols.Add(t.Symbol);
                if (!visited.Contains(t.To))
                    toVisit.Push(t.To);
            }
        }

        return symbols;
    }
}
