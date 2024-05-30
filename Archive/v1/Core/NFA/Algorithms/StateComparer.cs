using System.Diagnostics.CodeAnalysis;

namespace Core.NFA.Algorithms;

public class StateComparer : IEqualityComparer<State>
{
    public bool Equals(State? x, State? y)
    {
        return x != null && y != null && x.nodes.SetEquals(y.nodes);
    }

    public int GetHashCode([DisallowNull] State obj)
    {
        return obj.nodes.GetHashCode();
    }
}
