using Core.Common;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using Core.RegularExpressions.Nodes;

namespace Core.Graphs.Algorithms;

public class RegexToNFAVisitor : IVisitor<Graph>
{
    public static Graph Accept(Regex regex)
    {
        var visitor = new RegexToNFAVisitor();
        return regex.Node.Accept(visitor);
    }

    public Graph Visit(AnyCharacterNode node)
    {
        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };
        graph.Start.AddAnyTransition(graph.End.First());

        return graph;
    }

    public Graph Visit(CharacterNode node)
    {
        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };
        graph.Start.AddTransition(graph.End.First(), new Symbol(new CharacterSet(node.Value)));

        return graph;
    }

    public Graph Visit(CharacterSetNode node)
    {
        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };
        graph.Start.AddTransition(graph.End.First(), new Symbol(node.GetCharacterSet()));

        return graph;
    }

    public Graph Visit(AlternationNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        
        left.End.First().IsFinal = false;
        right.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };

        graph.Start.AddEpsilonTransition(left.Start);
        graph.Start.AddEpsilonTransition(right.Start);

        left.End.First().AddEpsilonTransition(graph.End.First());
        right.End.First().AddEpsilonTransition(graph.End.First());


        return graph;
    }

    public Graph Visit(ConcatenationNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        
        left.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = left.Start,
            End = [right.End.First()]
        };

        left.End.First().AddEpsilonTransition(right.Start);

        return graph;
    }

    public Graph Visit(StarNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());
        child.End.First().AddEpsilonTransition(child.Start);

        graph.Start.AddEpsilonTransition(graph.End.First());

        return graph;
    }

    public Graph Visit(PlusNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());
        child.End.First().AddEpsilonTransition(child.Start);

        return graph;
    }

    public Graph Visit(OptionalNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new Node(),
            End = [new Node(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());

        graph.Start.AddEpsilonTransition(graph.End.First());

        return graph;
    }
}
