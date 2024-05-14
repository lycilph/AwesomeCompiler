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

    public override bool Equals(Node? other)
    {
        if (other != null && other is StarNode star)
            return Child != null && Child.Equals(star.Child);
        else
            return false;
    }
}
