using Core.NFA;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("Single character node [{_value}]")]
public class MatchSingleCharacterNode : RegexNode
{
    public char Value { get; private set; }

    public MatchSingleCharacterNode(char value)
    {
        Value = value;
    }

    public override void ReplaceNode(RegexNode oldNode, RegexNode newNode)
    {
        throw new InvalidOperationException("A MatchSingleCharacterNode cannot replace a node");
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        if (input.Count > 0 && input.First() == Value)
        {
            input.RemoveAt(0);
            return true;
        }
        return false;
    }

    public override Graph ConvertToNFA()
    {
        var graph = new Graph
        {
            Start = new Node(),
            End = new Node(true)
        };
        graph.Start.AddTransition(graph.End, [Value], Value.ToString());

        return graph;
    }

    public override bool Equals(RegexNode? other)
    {
        if (other != null && other is MatchSingleCharacterNode sc)
            return Value == sc.Value;
        else
            return false;
    }
}
