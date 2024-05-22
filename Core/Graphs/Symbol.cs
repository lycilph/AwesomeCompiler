using Core.Common;
using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("IsEpsilon={IsEpsilon}, IsAny={IsAny}, Chars={Chars.Count}")]
public class Symbol
{
    public bool IsEpsilon { get; } = false;
    public bool IsAny { get; } = false;
    public CharacterSet Set { get; } = new();

    public Symbol(bool epsilon, bool any)
    {
        IsEpsilon = epsilon;
        IsAny = any;
    }
    public Symbol(CharacterSet set)
    {
        Set = set;
    }

    public override string ToString()
    {
        return this switch
        {
            _ when IsEpsilon => "Epsilon",
            _ when IsAny => "Any",
            _ => Set.ToString(),
        };
    }
}