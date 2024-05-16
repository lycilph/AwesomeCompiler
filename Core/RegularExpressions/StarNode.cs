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

    public override NFA.Graph ConvertToNFA()
    {
        var graph = Child!.ConvertToNFA();

        var start = new NFA.Node();
        var end = new NFA.Node(true);

        start.AddEmptyTransition(graph.Start);
        end.AddEmptyTransition(start);
        graph.End.AddEmptyTransition(end);
        start.AddEmptyTransition(end);

        graph.Start = start;
        graph.End.IsFinal = false;
        graph.End = end;

        return graph;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is StarNode star)
            return Child != null && Child.Equals(star.Child);
        else
            return false;
    }
}
