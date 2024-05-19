using Core.RegularExpressions.Nodes;

namespace Core.RegularExpressions.Algorithms;

public class PrintAstVisitor : IVisitor
{
    private int indent = 0;

    private string Get(RegexNode node)
    {
        string? parentId;
        if (node.Parent is RegexNode rn)
            parentId = rn.Id.ToString();
        else if (node.Parent is Regex r)
            parentId = "root";
        else
            parentId = string.Empty;

        return new string(' ', indent) + node.GetType().Name + $" (id: {node.Id}, parent id: {parentId})";
    }

    public void Visit(AnyCharacterNode node)
    {
        Console.WriteLine(Get(node));
    }

    public void Visit(CharacterNode node)
    {
        Console.WriteLine(Get(node)+$" - {node}");
    }

    public void Visit(CharacterSetNode node)
    {
        Console.WriteLine(Get(node) + $" - [{node}]");
    }

    public void Visit(AlternationNode node)
    {
        Console.WriteLine(Get(node));
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }

    public void Visit(ConcatenationNode node)
    {
        Console.WriteLine(Get(node));
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }

    public void Visit(StarNode node)
    {
        Console.WriteLine(Get(node));
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(PlusNode node)
    {
        Console.WriteLine(Get(node));
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(OptionalNode node)
    {
        Console.WriteLine(Get(node));
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }
}
