using Core.NFA;
using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions;

public abstract class RegexNode : IEquatable<RegexNode>
{
    public RegexNode? Parent { get; set; }

    public abstract void ReplaceNode(RegexNode oldNode, RegexNode newNode);

    public abstract void Accept(IVisitor visitor);
    public abstract bool IsMatch(List<char> input);
    public abstract Graph ConvertToNFA();

    public abstract bool Equals(RegexNode? other);
    public override bool Equals(object? obj)
    {
        return Equals(obj as RegexNode);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this);
    }
}
