using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

// nq = no qoutes around the string
[DebuggerDisplay("Character set node [{Label}] {_chars.Count} characters")]
public class CharacterSetNode : Node
{
    private readonly HashSet<char> _chars = [];
    public bool IsNegativeSet { get; set; }
    public string Label { get; set; } = string.Empty;

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

    public int Count() => _chars.Count;

    public void AddRange(char start, char end)
    {
        for (char c = start; c <= end; c++)
            _chars.Add(c);
        Label += $"{start}-{end}";
    }

    public void AddCharacter(char c)
    {
        _chars.Add(c);
        Label += c;
    }

    public void AddSet(CharacterSetNode set)
    {
        _chars.UnionWith(set._chars);
        Label += set.Label;
    }

    public override void ReplaceNode(Node oldNode, Node newNode)
    {
        throw new InvalidOperationException("A CharacterSetNode cannot replace a node");
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

    public override NFA.Graph ConvertToNFA()
    {
        var graph = new NFA.Graph
        {
            Start = new NFA.Node(),
            End = new NFA.Node(true)
        };
        graph.Start.AddTransition(graph.End, _chars, Label);

        return graph;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is CharacterSetNode set)
            return _chars.SetEquals(set._chars) && IsNegativeSet == set.IsNegativeSet;
        else
            return false;
    }
}
