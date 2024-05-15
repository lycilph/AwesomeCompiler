using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

// nq = no qoutes around the string _pattern
[DebuggerDisplay("Character set node [{_chars.Count} characters]")]
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
        AddRange(parts[0][0], parts[1][0]);
    }

    public void AddRange(char start, char end)
    {
        for (char c = start; c <= end; c++)
            AddCharacter(c);
    }

    public void AddCharacter(char c) => _chars.Add(c);

    public void AddSet(CharacterSetNode set) => _chars.UnionWith(set._chars);

    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        throw new NotImplementedException();
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        if (input.Count > 0 && _chars.Contains(input.First()))
        {
            input.RemoveAt(0);
            return true;
        }
        return false;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is CharacterSetNode set)
            return _chars.SetEquals(set._chars) && IsNegativeSet == set.IsNegativeSet;
        else
            return false;
    }
}
