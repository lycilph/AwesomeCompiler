using Core.Common;
using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("{ToString()}")]
public class Symbol : IEquatable<Symbol>
{
    // Used only for hash code generation
    private const string epsilon = "epsilon";
    private const string any = "any";

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

    public bool Equals(Symbol? other)
    {
        if (other is null)
            return false;

        return (IsEpsilon && other.IsEpsilon) ||
               (IsAny && other.IsAny) ||
               Set.Equals(other.Set);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Symbol);
    }

    public override int GetHashCode()
    {
        return this switch
        {
            _ when IsEpsilon => epsilon.GetHashCode(),
            _ when IsAny => any.GetHashCode(),
            _ => Set.GetHashCode()
        };
    }

    public static bool operator ==(Symbol? left, Symbol? right) => Equals(left, right);
    public static bool operator !=(Symbol? left, Symbol? right) => !Equals(left, right);
}