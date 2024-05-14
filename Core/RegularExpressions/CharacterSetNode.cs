using System.Diagnostics;

namespace Core.RegularExpressions;

// nq = no qoutes around the string _pattern
[DebuggerDisplay("Character set node [{_pattern, nq}]")]
public class CharacterSetNode : Node
{
    private readonly string _pattern;

    public CharacterSetNode(string pattern)
    {
        _pattern = pattern;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is CharacterSetNode set) 
            return _pattern.Equals(set._pattern);
        else
            return false;
    }
}
