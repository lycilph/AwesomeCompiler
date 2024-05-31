using AwesomeCompilerCore.Common;
using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Nodes;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using Microsoft.Msagl.Drawing;

namespace AwesomeCompilerIDE;

public class RegexGraphVisitor : IVisitor<Node>
{
    public Graph Graph { get; private set; } = new();

    public Node Visit(Regex node)
    {
        var root = Graph.AddNode($"Root ({node.Id})");
        var child = node.Root.Accept(this);

        Graph.AddEdge(root.Id, child.Id);

        return root;
    }

    public Node Visit(AnyCharacterRegexNode node)
    {
        return Graph.AddNode($". ({node.Id})");
    }

    public Node Visit(CharacterRegexNode node)
    {
        return Graph.AddNode($"{node.Value.CharToString()} ({node.Id})");
    }

    public Node Visit(CharacterSetRegexNode node)
    {
        return Graph.AddNode($"{node} ({node.Id})");
    }

    public Node Visit(AlternationRegexNode node)
    {
        var alternation = Graph.AddNode($"| ({node.Id})");
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        Graph.AddEdge(alternation.Id, right.Id);
        Graph.AddEdge(alternation.Id, left.Id);

        return alternation;
    }

    public Node Visit(ConcatenationRegexNode node)
    {
        var concatenation = Graph.AddNode($"Concat ({node.Id})");
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        Graph.AddEdge(concatenation.Id, right.Id);
        Graph.AddEdge(concatenation.Id, left.Id);

        return concatenation;
    }

    public Node Visit(StarRegexNode node)
    {
        var star = Graph.AddNode($"* ({node.Id})");
        var child = node.Child.Accept(this);

        Graph.AddEdge(star.Id, child.Id);

        return star;
    }

    public Node Visit(PlusRegexNode node)
    {
        var plus = Graph.AddNode($"+ ({node.Id})");
        var child = node.Child.Accept(this);

        Graph.AddEdge(plus.Id, child.Id);

        return plus;
    }

    public Node Visit(OptionalRegexNode node)
    {
        var optional = Graph.AddNode($"? ({node.Id})");
        var child = node.Child.Accept(this);

        Graph.AddEdge(optional.Id, child.Id);

        return optional;
    }
}