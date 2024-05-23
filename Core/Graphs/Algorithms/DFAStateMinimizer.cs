namespace Core.Graphs.Algorithms;

public class DFAStateMinimizer
{
    private HashSet<Symbol> symbols = [];

    public Node Execute(Node dfa)
    {
        // Find input language symbols (the match all is implicitly included further on)
        symbols = Collector.CollectSymbols(dfa);

        // Setup initial state
        var allNodes = Collector.CollectNodes(dfa);
        var partition = InitialPartition(allNodes);

        // Iterate over partition until it doesn't change
        var newPartition = PartitionSets(partition);
        while (newPartition.Count != partition.Count)
        {
            partition = newPartition;
            newPartition = PartitionSets(partition);
        }

        // Create new graph from final partitions
        var minimizedDfa = CreateGraph(newPartition, dfa);
        return minimizedDfa;
    }
    
    private Node CreateGraph(HashSet<HashSet<Node>> partition, Node start)
    {
        // Create nodes
        var setToNodeMap = new Dictionary<HashSet<Node>, Node>(HashSet<Node>.CreateSetComparer());
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

    private HashSet<HashSet<Node>> PartitionSets(HashSet<HashSet<Node>> partition)
    {
        var newPartition = new HashSet<HashSet<Node>>();

        // Iterate over each set in the partition to see if it needs to be split further
        foreach (var set in partition)
        {
            // We cannot further partition a set of 1 node
            if (set.Count == 1)
            {
                newPartition.Add(set);
                continue;
            }

            // We partion a set into to new sets, those that are equivalent and the rest
            var equivalentSet = new HashSet<Node>();
            var nonEquivalentSet = new HashSet<Node>();

            // Find pairs in the set, to check for equivalence
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
                    // Figure out where to add the items 
                    equivalentSet.Add(pair.Item1);
                    nonEquivalentSet.Add(pair.Item2);
                }
            }

            // Add the new sets to the new partion (if non-empty)
            newPartition.Add(equivalentSet);
            if (nonEquivalentSet.Count > 0)
                newPartition.Add(nonEquivalentSet);
        }

        return newPartition;
    }

    private static bool IsTransitionsEquivalent(HashSet<HashSet<Node>> partition, Node n1, Node n2, Symbol s)
    {
        // Find transition for each node on a given symbol
        var t1 = n1.Transitions.Where(t => t.Symbol == s).FirstOrDefault();
        var t2 = n2.Transitions.Where(t => t.Symbol == s).FirstOrDefault();

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

    private static HashSet<Node> FindNodeSet(HashSet<HashSet<Node>> partition, Node n)
    {
        foreach (var set in partition)
            if (set.Contains(n))
                return set;
        throw new ArgumentException("A node should always belong to the partition");
    }

    private static List<Tuple<Node, Node>> GetPairs(HashSet<Node> set)
    {
        var nodeList = set.ToList();
        var pairs = new List<Tuple<Node, Node>>();
        for (var i = 0; i < nodeList.Count; i++)
            for (int j = i + 1; j < nodeList.Count; j++)
                pairs.Add(Tuple.Create(nodeList[i], nodeList[j]));
        return pairs;
    }

    private HashSet<HashSet<Node>> InitialPartition(IEnumerable<Node> allNodes)
    {
        var finalStates = new HashSet<Node>(allNodes.Where(n => n.IsFinal));
        var nonFinalStates = new HashSet<Node>(allNodes.Where(n => !n.IsFinal));

        return [nonFinalStates, finalStates];
    }
}
