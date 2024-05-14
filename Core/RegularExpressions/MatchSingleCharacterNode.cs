using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("Single character node [{_value}]")]
public class MatchSingleCharacterNode : Node
{
    private readonly char _value;

    public MatchSingleCharacterNode(char value)
    {
        _value = value;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is MatchSingleCharacterNode sc)
            return _value == sc._value;
        else
            return false;
    }
}
