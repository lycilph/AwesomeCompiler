namespace Core.RegularExpressions;

public class SequenceNode : Node
{
    private readonly List<Node> _children = [];

    public void Add(Node node) => _children.Add(node);

    public void ReplaceLast(Node node)
    {
        _children.RemoveAt(_children.Count-1);
        Add(node);
    }

    public Node Last() => _children.Last();

    public override bool IsMatch(List<char> input)
    {
        foreach (Node node in _children)
            if (!node.IsMatch(input))
                return false;
        return true;
    }

    public override bool Equals(Node? other)
    {
        if (other != null && other is SequenceNode seq) 
        {
            if (_children.Count != seq._children.Count)
                return false;

            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                var other_child = seq._children[i];

                if (!child.Equals(other_child))
                    return false;
            }
        }
        else
            return false;

        return true;
    }
}
