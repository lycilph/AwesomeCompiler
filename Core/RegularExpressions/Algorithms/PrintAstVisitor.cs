namespace Core.RegularExpressions.Algorithms;

public class PrintAstVisitor : IVisitor
{
    private int indent = 0;

    private string Str() => new(' ', indent);

    public void Visit(CharacterNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name} - {node.Value}");
    }

    public void Visit(AlternationNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name}");
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }

    public void Visit(ConcatenationNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name}");
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }

    public void Visit(StarNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name}");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(PlusNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name}");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(OptionalNode node)
    {
        Console.WriteLine($"{Str()}{node.GetType().Name}");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }
}
