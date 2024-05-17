namespace Core.NFA.Algorithms;

public class StateMinimization
{
    public Node Execute(Node start)
    {
        // Find input language symbols (the match all is implicitly included further on)
        var symbols = SymbolCollector.Collect(start);

        // Setup initial state
        var allNodes = FindAllNodes(start);
        var partition = InitialPartition(allNodes);

        // Iterate over partition until it doesn't change
        var newPartition = PartitionSets(partition, symbols);
        while (newPartition.Count != partition.Count)
        {
            partition = newPartition;
            newPartition = PartitionSets(partition, symbols);
        }

        // Create new graph
        return CreateGraph(partition, start, symbols);
    }

    private Node CreateGraph(HashSet<NodeSet> partition, Node start, HashSet<Symbol> symbols)
    {
        // Create nodes
        var setToNodeMap = new Dictionary<NodeSet, Node>(NodeSet.CreateSetComparer());
        foreach (var set in partition)
        {
            var isFinal = set.Where(n => n.IsFinal).Any();
            setToNodeMap[set] = new Node(isFinal);
        }

        // Create transitions
        foreach (var p in setToNodeMap)
        {
            var set = p.Key;
            var node = p.Value;

            var setNode = set.First(); // Since states are equivalent, any node in a set should work
            foreach (var t in setNode.Transitions)
            {
                // Find node to transition to
                var toNode = setToNodeMap.First(p => p.Key.Contains(t.To)).Value;
                node.AddTransition(toNode, t.Symbol);
            }
        }

        return setToNodeMap.First(p => p.Key.Contains(start)).Value;
    }

    private HashSet<NodeSet> PartitionSets(HashSet<NodeSet> partition, HashSet<Symbol> symbols)
    {
        var newPartition = new HashSet<NodeSet>();

        foreach (var set in partition)
        {
            // We cannot further partition a set of 1 node
            if (set.Count == 1)
            {
                newPartition.Add(set);
                continue;
            }

            var equivalentSet = new NodeSet();
            var nonEquivalentSet = new NodeSet();

            // Find pairs in the set
            var pairs = GetPairs(set);

            // Check if each pair is equivalent
            foreach (var pair in pairs)
            {
                // Skip pairs that have already been evaluated
                var totalSet = equivalentSet.Union(nonEquivalentSet);
                if (totalSet.Contains(pair.Item1) && totalSet.Contains(pair.Item2))
                    continue;

                var equivalent = symbols
                    .Select(s => IsTransitionsEquivalent(partition, pair.Item1, pair.Item2, s))
                    .All(x => x); // Checks if all items are true;

                if (equivalent)
                {
                    equivalentSet.Add(pair.Item1);
                    equivalentSet.Add(pair.Item2);
                }
                else
                {
                    equivalentSet.Add(pair.Item1);
                    nonEquivalentSet.Add(pair.Item2);
                }
            }

            newPartition.Add(equivalentSet);
            if (nonEquivalentSet.Count > 0)
                newPartition.Add(nonEquivalentSet);
        }

        return newPartition;
    }

    private static bool IsTransitionsEquivalent(HashSet<NodeSet> partition, Node n1, Node n2, Symbol s)
    {
        // Find transition for each node on a given symbol
        var t1 = n1.Transitions.Where(t => t.Match(s)).FirstOrDefault();
        var t2 = n2.Transitions.Where(t => t.Match(s)).FirstOrDefault();

        if (t1 == null && t2 == null)
            return true;
        else if (t1 != null && t2 != null)
        {
            var s1 = FindNodeSet(partition, t1.To);
            var s2 = FindNodeSet(partition, t2.To);
            return s1.SetEquals(s2);
        }
        else
            return false;
    }

    private static NodeSet FindNodeSet(HashSet<NodeSet> partition, Node n)
    {
        foreach (var set in partition)
            if (set.Contains(n))
                return set;
        throw new ArgumentException("A node should always belong to the partition");
    }

    private static List<Tuple<Node, Node>> GetPairs(NodeSet set)
    {
        var nodeList = set.ToList();
        var pairs = new List<Tuple<Node, Node>>();
        for (var i = 0; i < nodeList.Count; i++)
            for (int j = i + 1; j < nodeList.Count; j++)
                pairs.Add(Tuple.Create(nodeList[i], nodeList[j]));
        return pairs;
    }

    private HashSet<NodeSet> InitialPartition(IEnumerable<Node> allNodes)
    {
        var finalStates = new NodeSet(allNodes.Where(n => n.IsFinal));
        var nonFinalStates = new NodeSet(allNodes.Where(n => !n.IsFinal));

        return [nonFinalStates, finalStates];
    }

    private IEnumerable<Node> FindAllNodes(Node start)
    {
        var visited = new NodeSet();
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