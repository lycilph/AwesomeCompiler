
using System.Diagnostics.CodeAnalysis;

namespace Core.Graphs.Algorithms;

internal class DFANodeComparer : IEqualityComparer<Node>
{
    public bool Equals(Node? x, Node? y)
    {
        return x != null && y != null && x.Nodes.SetEquals(y.Nodes);
    }

    public int GetHashCode([DisallowNull] Node obj)
    {
        if (obj.Nodes.Count == 0)
            return 0;

        int hash = 17;
        foreach (var n in obj.Nodes)
            hash = hash ^ HashCode.Combine(n.Id.GetHashCode());
        return hash;
    }
}