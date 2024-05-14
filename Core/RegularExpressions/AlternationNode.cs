using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("| node")]
public class AlternationNode : Node
{
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public override bool Equals(Node? other)
    {
        if (other != null && other is AlternationNode alternation)
            return Left != null && alternation.Left != null && Left.Equals(alternation.Left) &&
                   Right != null && alternation.Right != null && Right.Equals(alternation.Right);
        else
            return false;
    }
}
