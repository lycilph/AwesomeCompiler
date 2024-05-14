using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("? node")]
public class OptionalNode : Node
{
    public Node? Child { get; set; }

    public OptionalNode(Node? child = null)
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
        Child!.IsMatch(input);
        return true;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is OptionalNode optional)
            return Child != null && Child.Equals(optional.Child);
        else
            return false;
    }
}
