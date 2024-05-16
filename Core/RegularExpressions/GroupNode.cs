using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("group node")]
public class GroupNode : Node
{
    private Node _child = null!;

    public Node Child
    {
        get => _child;
        private set
        {
            _child = value;
            if (_child != null)
                _child.Parent = this;
        }
    }
    
    public GroupNode(Node child)
    {
        Child = child;
    }

    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        Child = newNode;
    }

    public override void Accept(IVisitor visitor)
    {
        Child.Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input) => Child.IsMatch(input);

    public override NFA.Graph ConvertToNFA()
    {
        return Child.ConvertToNFA();
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is GroupNode group)
            return Child.Equals(group.Child);
        else
            return false;
    }
}
