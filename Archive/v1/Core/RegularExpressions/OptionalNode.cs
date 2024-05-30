using Core.NFA;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("? node")]
public class OptionalNode : RegexNode
{
    private RegexNode? child;

    public RegexNode? Child { get => child; 
        set
        { 
            child = value; 
            if (child != null)
                child.Parent = this;
        }}

    public OptionalNode(RegexNode? child = null)
    {
        Child = child;
        if (Child != null)
            Child.Parent = this;
    }

    public override void ReplaceNode(RegexNode oldNode, RegexNode newNode)
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
        Child!.IsMatch(input);
        return true;
    }

    public override Graph ConvertToNFA()
    {
        var graph = Child!.ConvertToNFA();

        var start = new Node();
        var end = new Node(true);

        start.AddEmptyTransition(graph.Start);
        graph.End.AddEmptyTransition(end);
        start.AddEmptyTransition(end);

        graph.Start = start;
        graph.End.IsFinal = false;
        graph.End = end;

        return graph;
    }

    public override bool Equals(RegexNode? other)
    {
        if (other != null && other is OptionalNode optional)
            return Child != null && Child.Equals(optional.Child);
        else
            return false;
    }
}
