using Core.NFA;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions;

[DebuggerDisplay("group node")]
public class GroupNode : RegexNode
{
    private RegexNode _child = null!;

    public RegexNode Child
    {
        get => _child;
        private set
        {
            _child = value;
            if (_child != null)
                _child.Parent = this;
        }
    }
    
    public GroupNode(RegexNode child)
    {
        Child = child;
    }

    public override void ReplaceNode(RegexNode oldNode, RegexNode newNode)
    {
        Child = newNode;
    }

    public override void Accept(IVisitor visitor)
    {
        Child.Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input) => Child.IsMatch(input);

    public override Graph ConvertToNFA()
    {
        return Child.ConvertToNFA();
    }

    public override bool Equals(RegexNode? other)
    {
        if (other != null && other is GroupNode group)
            return Child.Equals(group.Child);
        else
            return false;
    }
}
