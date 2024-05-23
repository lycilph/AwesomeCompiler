namespace Core.Graphs.Algorithms;

// This generates a DFA from an NFA using Thompsons Construction algorithm
public class NFAToDFACreator
{
    private readonly Dictionary<HashSet<Node>, Node> known_states = new(HashSet<Node>.CreateSetComparer());
    private readonly HashSet<Symbol> symbols = [];

    public Node Execute(Node nfa)
    {
        // Clear state
        known_states.Clear();
        symbols.Clear();

        // Find symbols
        FindSymbols(nfa);

        foreach (var s in symbols)
            Console.WriteLine($"Found symbol {s}");

        // Construct new states
        var start = ConstructStates(nfa);

        // Mark final states
        MarkFinalStates();

        // Return DFA
        return start;
    }
    
    private void MarkFinalStates()
    {
        foreach (var state in known_states.Values)
            foreach (var node in state.Nodes)
                if (node.IsFinal)
                    state.IsFinal = true;
    }

    private void FindSymbols(Node node)
    {
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
    }

    private Node ConstructStates(Node nfa)
    {
        var visited = new HashSet<Node>(new DFANodeComparer());
        var toVisit = new Stack<Node>();

        // Find close of first state to initialize algorithm
        var start = EpsilonClosue([nfa]);
        known_states.Add(start.Nodes, start);
        toVisit.Push(start);

        while (toVisit.Count > 0)
        {
            var node = toVisit.Pop();
            if (visited.Contains(node))
                continue;
            visited.Add(node);

            foreach (var symbol in symbols)
            {
                var moveClosure = MoveClosure(node, symbol);

                // If this is an empty state, then continue
                if (moveClosure.Nodes.Count == 0)
                    continue;

                // Check if this state already exists (if so use the existing one)
                if (known_states.TryGetValue(moveClosure.Nodes, out Node? knownNode))
                    moveClosure = knownNode;
                else
                    known_states.Add(moveClosure.Nodes, moveClosure);

                // Add the transition to the new node
                node.AddTransition(moveClosure, symbol);

                toVisit.Push(moveClosure);
            }
        }

        return start;
    }

    private Node MoveClosure(Node node, Symbol symbol)
    {
        // Starting from the nodes in the given input node, find all nodes, that it has a transition to on the given symbol
        var nodes = node.Nodes
            .SelectMany(n => n.Transitions)
            .Where(t => t.Symbol == symbol)
            .Select(t => t.To)
            .Distinct()
            .ToList();

        // Find epsilon-closure of these states
        var closure = EpsilonClosue(nodes);
        return closure;
    }

    private Node EpsilonClosue(IEnumerable<Node> nodes)
    {
        var closure = new Node();

        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        // Initialize list
        foreach (var n in nodes)
            toVisit.Push(n);

        while (toVisit.Count > 0)
        {
            var node = toVisit.Pop();
            visited.Add(node);

            closure.Nodes.Add(node);

            foreach (var t in node.Transitions)
                if (t.Symbol.IsEpsilon && !visited.Contains(t.To))
                    toVisit.Push(t.To);
        }

        return closure;
    }
}
