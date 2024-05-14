namespace Core.RegularExpressions;

public abstract class Node : IEquatable<Node>
{
    public abstract bool Equals(Node? other);
}
