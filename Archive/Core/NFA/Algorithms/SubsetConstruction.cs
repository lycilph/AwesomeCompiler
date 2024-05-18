namespace Core.NFA.Algorithms;

public class SubsetConstruction
{
    private readonly Dictionary<HashSet<Node>, State> knownStates = new(HashSet<Node>.CreateSetComparer());

    public Node Execute(Graph nfa)
    {
        knownStates.Clear();

        // Find input language symbols (the match all is implicitly included further on)
        var symbols = SymbolCollector.Collect(nfa.Start);

        // Construct states
        var start = ConstructStates(nfa.Start, symbols);

        // Mark final states
        MarkFinalStates(nfa.End);

        return ConvertToNodeGraph(start);
    }

    private Node ConvertToNodeGraph(State start)
    {
        var stateToNodeMap = new Dictionary<State, Node>();

        // Create nodes for all states
        foreach (var state in knownStates.Values)
            stateToNodeMap[state] = new Node(state.IsFinal);

        // Create transitions for all states
        foreach (var p in stateToNodeMap)
        {
            var state = p.Key;

            foreach (var transition in state.Transitions)
            {
                var node = p.Value;
                var toNode = stateToNodeMap[transition.To];
                node.AddTransition(toNode, transition.Symbol);
            }
        }

        return stateToNodeMap[start];
    }

    private void MarkFinalStates(Node endNode)
    {
        foreach (var state in knownStates.Values)
            if (state.Contains(endNode))
                state.IsFinal = true;
    }

    private State ConstructStates(Node startNode, IEnumerable<Symbol> symbols)
    {
        var visited = new HashSet<State>(new StateComparer());
        var toVisit = new Stack<State>();

        // Find close of first state to initialize algorithm
        var start = EpsilonClosue(startNode);
        knownStates.Add(start.nodes, start);
        toVisit.Push(start);

        while (toVisit.Count > 0)
        {
            var s = toVisit.Pop();
            if (visited.Contains(s))
                continue;
            visited.Add(s);

            foreach (var symbol in symbols)
            {
                var m = Move(s, symbol);
                // If this is an empty state, then continue
                if (m.nodes.Count == 0)
                    continue;
                // Check if this state already exists (if so use the existing one)
                if (knownStates.TryGetValue(m.nodes, out State? knownM))
                    m = knownM;
                else
                    knownStates.Add(m.nodes, m);
                s.AddTransition(m, symbol);
                toVisit.Push(m);
            }
        }

        return start;
    }

    private State EpsilonClosue(Node n)
    {
        return EpsilonClosue([n]);
    }

    private State EpsilonClosue(IEnumerable<Node> nodes)
    {
        var closure = new State();

        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        // Initialize list
        foreach (var n in nodes)
            toVisit.Push(n);

        while (toVisit.Count > 0)
        {
            var node = toVisit.Pop();
            closure.Add(node);

            visited.Add(node);

            foreach (var t in node.Transitions)
                if (t.IsEpsilon() && !visited.Contains(t.To))
                    toVisit.Push(t.To);
        }

        return closure;
    }

    private State Move(State s, Symbol sym)
    {
        // Starting from nodes in state s, find all nodes, that has a transition on c
        var nodes = s.nodes
            .SelectMany(n => n.Transitions)
            .Where(t => t.Match(sym))
            .Select(t => t.To)
            .Distinct()
            .ToList();

        // Find epsilon-closure of these states
        var closure = EpsilonClosue(nodes);
        return closure;
    }
}
