namespace Core.RegularExpressions.Algorithms;

public class PrintAstVisitor : IVisitor
{
    private int indent = 0;

    private string Get(RegexNode node) => new string(' ', indent) + node.GetType().Name + $" (id: {node.Id})";

    public void Visit(CharacterNode node)
    {
        Console.WriteLine(Get(node)+$" - {node.Value}");
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
