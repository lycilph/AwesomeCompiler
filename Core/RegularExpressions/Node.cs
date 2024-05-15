using Core.NFA;
using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions;

public abstract class Node : IEquatable<Node>
{
    public Node? Parent { get; set; }

    public abstract void ReplaceNode(Node oldNode, Node newNode);

    public abstract void Accept(IVisitor visitor);
    public abstract bool IsMatch(List<char> input);
    public abstract Graph ConvertToNFA();

    public abstract bool Equals(Node? other);
    public override bool Equals(object? obj)
    {
        return Equals(obj as Node);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this);
    }
}
