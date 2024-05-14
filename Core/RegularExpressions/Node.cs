namespace Core.RegularExpressions;

public abstract class Node : IEquatable<Node>
{
    public abstract bool IsMatch(List<char> input);
    public abstract bool Equals(Node? other);
}
