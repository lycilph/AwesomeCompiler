using Core.RegularExpressions.Algorithms;
using System;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay(". node")]
public class MatchAnyCharacterNode : Node
{
    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        throw new NotImplementedException();
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

    public override NFA.Graph ConvertToNFA()
    {
        var graph = new NFA.Graph
        {
            Start = new NFA.Node(),
            End = new NFA.Node(true)
        };
        graph.Start.AddMatchAnyTransition(graph.End);

        return graph;
    }

    public override bool Equals(Node? other)
    {
        return other != null && other is MatchAnyCharacterNode;
    }
}
