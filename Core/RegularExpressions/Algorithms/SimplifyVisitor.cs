namespace Core.RegularExpressions.Algorithms;

public class SimplifyVisitor : IVisitor
{
    public void Visit(AnyCharacterNode node)
    {
    }

    public void Visit(CharacterNode node)
    {
    }

    public void Visit(CharacterSetNode node)
    {
    }

    public void Visit(AlternationNode node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);

        if (node.Parent == null)
            return;

        if (node.Left is AnyCharacterNode &&
            (node.Right is CharacterNode ||
             node.Right is CharacterSetNode)) 
        {
            node.Parent.Replace(node, node.Left);
        }

        if (node.Right is AnyCharacterNode &&
            (node.Left is CharacterNode ||
             node.Left is CharacterSetNode))
        {
            node.Parent.Replace(node, node.Right);
        }
    }

    public void Visit(ConcatenationNode node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
    }

    public void Visit(StarNode node)
    {
        node.Child.Accept(this);
    }

    public void Visit(PlusNode node)
    {
        node.Child.Accept(this);
    }

    public void Visit(OptionalNode node)
    {
        node.Child.Accept(this);
    }
}
