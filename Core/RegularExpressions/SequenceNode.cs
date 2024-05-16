using Core.NFA;
using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions;

public class SequenceNode : RegexNode
{
    public List<RegexNode> Children { get; private set; } = [];

    public void Add(RegexNode node) 
    {
        Children.Add(node);
        node.Parent = this;
    } 

    public void ReplaceLast(RegexNode node)
    {
        Children.RemoveAt(Children.Count-1);
        node.Parent = this;
        Add(node);
    }

    public int Count() => Children.Count;

    public RegexNode Last() => Children.Last();

    public override void ReplaceNode(RegexNode oldNode, RegexNode newNode)
    {
        var index = Children.IndexOf(oldNode);
        Children[index] = newNode;
        newNode.Parent = this;
    }

    public override void Accept(IVisitor visitor)
    {
        // Cannot use foreach here, as the list might change during enumeration
        for (int i = 0; i < Children.Count; i++)
            Children[i].Accept(visitor);
        visitor.Visit(this);
    }

    public override bool IsMatch(List<char> input)
    {
        foreach (RegexNode node in Children)
            if (!node.IsMatch(input))
                return false;
        return true;
    }

    public override Graph ConvertToNFA()
    {
        var result = Children.First().ConvertToNFA();

        for (int i = 1; i < Children.Count; i++)
        {
            var temp = Children[i].ConvertToNFA();

            result.End.IsFinal = false;
            result.End.AddEmptyTransition(temp.Start);
            result.End = temp.End;
        }

        return result;
    }

    public override bool Equals(RegexNode? other)
    {
        if (other != null && other is SequenceNode seq) 
        {
            if (Children.Count != seq.Children.Count)
                return false;

            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var other_child = seq.Children[i];

                if (!child.Equals(other_child))
                    return false;
            }
        }
        else
            return false;

        return true;
    }
}
