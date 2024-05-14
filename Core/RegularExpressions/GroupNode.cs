using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("group node")]
public class GroupNode : Node
{
    private readonly Node _child;

    public GroupNode(Node child)
    {
        _child = child;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is GroupNode group)
            return _child.Equals(group._child);
        else
            return false;
    }
}
