using System.Diagnostics;

namespace Core.RegularExpressions;

// nq = no qoutes around the string _pattern
[DebuggerDisplay("Character set node [{_pattern, nq}]")]
public class CharacterSetNode : Node
{
    private HashSet<char> _chars = [];
    public bool IsNegativeSet { get; set; }

    public CharacterSetNode() {}
    public CharacterSetNode(char start, char end, bool isNegativeSet = false)
    {
        IsNegativeSet = isNegativeSet;
        AddRange(start, end);
    }
    public CharacterSetNode(string range, bool isNegativeSet = false)
    {
        IsNegativeSet = isNegativeSet;
        var parts = range.Split('-');
        AddRange(parts[0][0], parts[2][0]);
    }

    public void AddRange(char start, char end)
    {
        for (char c = start; c <= end; c++)
            AddCharacter(c);
    }

    public void AddCharacter(char c) => _chars.Add(c);

    public override bool Equals(Node? other)
    {
        throw new NotImplementedException();
    }
}
