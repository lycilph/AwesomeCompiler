using AwesomeCompilerCore.Common;
using System.Diagnostics;

namespace AwesomeCompilerCore.Graphs;

[DebuggerDisplay("{ToString()}")]
public class Symbol : IEquatable<Symbol>
{
    // Used only for hash code generation
    private const string epsilon = "epsilon";
    private const string any = "any";

    public bool IsEpsilon { get; } = false;
    public bool IsAny { get; } = false;
    public CharSet Set { get; } = new();

    public Symbol(bool epsilon, bool any)
    {
        IsEpsilon = epsilon;
        IsAny = any;
    }
    public Symbol(CharSet set)
    {
        Set = set;
    }

    public HashSet<char> GetCharSet()
    {
        if (IsAny)
            return CharSet.All().ToHashSet();
        else
            return Set.ToHashSet();
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

    #region Equals
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

    
    public static bool operator ==(Symbol left, Symbol right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Symbol left, Symbol right)
    {
        return !(left == right);
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
    #endregion

    public static Symbol Epsilon() => new Symbol(true, false);
    public static Symbol Any() => new Symbol(false, true);
}