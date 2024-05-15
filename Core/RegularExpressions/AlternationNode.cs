using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("| node")]
public class AlternationNode : Node
{
    private Node? left;
    private Node? right;

    public Node? Left { get => left;
        set 
        {
            left = value;
            if (left != null)
                left.Parent = this;
        }}
    public Node? Right { get => right; 
        set
        { 
            right = value; 
            if (right != null)
                right.Parent = this;
        }}

    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        throw new NotImplementedException();
    }

    public override void Accept(IVisitor visitor)
    {
        Left!.Accept(visitor);
        Right!.Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        if (Left!.IsMatch(input))
            return true;
        if (Right!.IsMatch(input))
            return true;
        return false;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is AlternationNode alternation)
            return Left != null && alternation.Left != null && Left.Equals(alternation.Left) &&
                   Right != null && alternation.Right != null && Right.Equals(alternation.Right);
        else
            return false;
    }
}
