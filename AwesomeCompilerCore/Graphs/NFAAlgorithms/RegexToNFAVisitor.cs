using AwesomeCompilerCore.Common;
using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Nodes;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Xml.Linq;

namespace AwesomeCompilerCore.Graphs.NFAAlgorithms;

public class RegexToNFAVisitor : IVisitor<Graph>
{
    public Graph Visit(Regex node)
    {
        return node.Root.Accept(this);
    }

    public Graph Visit(AnyCharacterRegexNode node)
    {
        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };
        graph.Start.AddAnyTransition(graph.End.First());

        return graph;
    }

    public Graph Visit(CharacterRegexNode node)
    {
        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };
        graph.Start.AddTransition(graph.End.First(), new Symbol(new CharSet(node.Value)));

        return graph;
    }

    public Graph Visit(CharacterSetRegexNode node)
    {
        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };
        graph.Start.AddTransition(graph.End.First(), new Symbol(node.GetCharSet()));

        return graph;
    }

    public Graph Visit(AlternationRegexNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        left.End.First().IsFinal = false;
        right.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };

        graph.Start.AddEpsilonTransition(left.Start);
        graph.Start.AddEpsilonTransition(right.Start);

        left.End.First().AddEpsilonTransition(graph.End.First());
        right.End.First().AddEpsilonTransition(graph.End.First());

        return graph;
    }

    public Graph Visit(ConcatenationRegexNode node)
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

    public Graph Visit(StarRegexNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());
        child.End.First().AddEpsilonTransition(child.Start);

        graph.Start.AddEpsilonTransition(graph.End.First());

        return graph;
    }

    public Graph Visit(PlusRegexNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());
        child.End.First().AddEpsilonTransition(child.Start);

        return graph;
    }

    public Graph Visit(OptionalRegexNode node)
    {
        var child = node.Child.Accept(this);
        child.End.First().IsFinal = false;

        var graph = new Graph()
        {
            Start = new GraphNode(),
            End = [new GraphNode(true)]
        };

        graph.Start.AddEpsilonTransition(child.Start);
        child.End.First().AddEpsilonTransition(graph.End.First());

        graph.Start.AddEpsilonTransition(graph.End.First());

        return graph;
    }

    public static Graph Run(Regex regex)
    {
        var visitor = new RegexToNFAVisitor();
        return regex.Accept(visitor);
    }
}