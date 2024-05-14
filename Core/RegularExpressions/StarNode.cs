using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("* node")]
public class StarNode : Node
{
    public Node? Child { get; set; }

    public StarNode(Node? child = null)
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
        while (true)
        {
            if (!Child!.IsMatch(input))
                break;
        }
        return true;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is StarNode star)
            return Child != null && Child.Equals(star.Child);
        else
            return false;
    }
}
