namespace Core.NFA.Algorithms;

public class NodeSet : HashSet<Node> 
{
    public NodeSet() {}
    public NodeSet(IEnumerable<Node> collection) : base(collection) {}
}
