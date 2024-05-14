using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("+ node")]
public class PlusNode : Node
{
    public Node? Child { get; set; }

    public PlusNode(Node? child = null)
    {
        Child = child;
    }

    public override void Accept(IVisitor visitor)
    {
        Child!.Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        var result = false;
        while (true)
        {
            if (Child!.IsMatch(input))
                result = true;
            else
                break;
        }
        return result;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is PlusNode plus)
            return Child != null && Child.Equals(plus.Child);
        else
            return false;
    }
}
