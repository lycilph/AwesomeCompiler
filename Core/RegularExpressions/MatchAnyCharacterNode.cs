using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay(". node")]
public class MatchAnyCharacterNode : Node
{
    public override bool Equals(Node? other)
    {
        return other != null && other is MatchAnyCharacterNode;
    }
}
