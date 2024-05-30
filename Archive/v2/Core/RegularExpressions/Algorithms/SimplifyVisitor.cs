using Core.RegularExpressions.Nodes;

namespace Core.RegularExpressions.Algorithms;

public class SimplifyVisitor : IVisitor
{
    public void Visit(AnyCharacterNode node)
    {
    }

    public void Visit(CharacterNode node)
    {
        if (node.Parent == null)
            return;

        var set = new CharacterSetNode(node.Value);
        node.Parent.Replace(node, set);
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
            node.Right is CharacterSetNode) 
        {
            node.Parent.Replace(node, node.Left);
        }

        if (node.Right is AnyCharacterNode &&
            node.Left is CharacterSetNode)
        {
            node.Parent.Replace(node, node.Right);
        }

        if (node.Left is CharacterSetNode c1 &&
            node.Right is CharacterSetNode c2)
        {
            foreach (var element in c2.Elements)
                c1.Add(element);
            node.Parent.Replace(node, c1);
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
