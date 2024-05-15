using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("group node")]
public class GroupNode : Node
{
    private Node child;

    public Node Child
    {
        get => child;
        private set
        {
            child = value;
            if (child != null)
                child.Parent = this;
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

    public override bool Equals(Node? other)
    {
        if (other != null && other is GroupNode group)
            return Child.Equals(group.Child);
        else
            return false;
    }
}
