namespace Core.NFA.Algorithms;

public class SubsetConstruction
{
    public Graph Execute(Graph nfa)
    {
        var knownStates = new Dictionary<HashSet<Node>, State>(HashSet<Node>.CreateSetComparer());
        var visited = new HashSet<State>(new StateComparer());
        var toVisit = new Stack<State>();

        var start = EpsilonClosue(nfa.Start);
        knownStates.Add(start.nodes, start);
        toVisit.Push(start);

        // Find all "symbols" in the language here (ie. all symbols on transitions)

        while (toVisit.Count > 0)
        {
            var s = toVisit.Pop();
            if (visited.Contains(s))
                continue;
            visited.Add(s);

            var moveA = Move(s, 'a');
            // Check if this state already exists (if so use the existing one)
            if (knownStates.TryGetValue(moveA.nodes, out State? valueA))
                moveA = valueA;
            else
                knownStates.Add(moveA.nodes, moveA);
            s.AddTransition(moveA, 'a');
            toVisit.Push(moveA);

            var moveB = Move(s, 'b');
            // Check if this state already exists (if so use the existing one)
            if (knownStates.TryGetValue(moveB.nodes, out State? valueB))
                moveB = valueB;
            else
                knownStates.Add(moveB.nodes, moveB);
            toVisit.Push(moveB);
            s.AddTransition(moveB, 'b');
        }

        // Find final (or accepting) states (ie. all states that contains a final node)

        return new Graph();
    }

    private State Move(State s, char c)
    {
        // Starting from nodes in state s, find all nodes, that has a transition on c
        var nodes = s.nodes
            .SelectMany(n => n.Transitions)
            .Where(t => t.Match(c))
            .Select(t => t.To)
            .Distinct()
            .ToList();

        var closure = EpsilonClosue(nodes);
        return closure;
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
                if (t.Epsilon && !visited.Contains(t.To))
                    toVisit.Push(t.To);
        }

        return closure;
    }
}
