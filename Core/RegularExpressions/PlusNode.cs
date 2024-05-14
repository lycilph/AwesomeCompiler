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

    public override bool Equals(Node? other)
    {
        if (other != null && other is PlusNode plus)
            return Child != null && Child.Equals(plus.Child);
        else
            return false;
    }
}
