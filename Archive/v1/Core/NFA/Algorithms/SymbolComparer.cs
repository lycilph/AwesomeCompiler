using System.Diagnostics.CodeAnalysis;

namespace Core.NFA.Algorithms;

public class SymbolComparer : IEqualityComparer<Symbol>
{
    private const string epsilon = "epsilon";
    private const string any = "any";

    public bool Equals(Symbol? x, Symbol? y)
    {
        var result = x != null && y != null &&
            ((x.isEpsilon && y.isEpsilon) ||
             (x.isAny && y.isAny) ||
             x.chars.SetEquals(y.chars));
        return result;
    }

    public int GetHashCode([DisallowNull] Symbol obj)
    {
        if (obj.isEpsilon)
            return epsilon.GetHashCode();
        else if (obj.isAny)
            return any.GetHashCode();
        else
        {
            if (obj.chars.Count == 0) return 0;
            int hashCode = 0;
            foreach (var c in obj.chars)
                hashCode = hashCode ^ HashCode.Combine(c);
            return hashCode;
        }
    }
}
