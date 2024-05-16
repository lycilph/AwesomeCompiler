using Core.NFA;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay(". node")]
public class MatchAnyCharacterNode : RegexNode
{
    public override void ReplaceNode(RegexNode oldNode, RegexNode newNode)
    {
        throw new InvalidOperationException("A MatchAnyCharacterNode cannot replace a node");
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        if (input.Count > 0)
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
        graph.Start.AddAnyTransition(graph.End);

        return graph;
    }

    public override bool Equals(RegexNode? other)
    {
        return other != null && other is MatchAnyCharacterNode;
    }
}
