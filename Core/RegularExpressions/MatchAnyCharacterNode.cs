using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay(". node")]
public class MatchAnyCharacterNode : Node
{
    public override bool IsMatch(List<char> input)
    {
        if (input.Count > 0)
        {
            input.RemoveAt(0);
            return true;
        }
        return false;
    }

    public override bool Equals(Node? other)
    {
        return other != null && other is MatchAnyCharacterNode;
    }
}
