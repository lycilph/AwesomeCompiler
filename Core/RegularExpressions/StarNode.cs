using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("* node")]
public class StarNode : Node
{
    private Node? child;

    public Node? Child { get => child; 
        set 
        { 
            child = value; 
            if (child != null)
                child.Parent = this;
        }}

    public StarNode(Node? child = null)
    {
        Child = child;
        if (Child != null)
            Child.Parent = this;
    }

    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        Child = newNode;
    }

    public override void Accept(IVisitor visitor)
    {
        Child!.Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        while (true)
        {
            if (!Child!.IsMatch(input))
                break;
        }
        return true;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is StarNode star)
            return Child != null && Child.Equals(star.Child);
        else
            return false;
    }
}
