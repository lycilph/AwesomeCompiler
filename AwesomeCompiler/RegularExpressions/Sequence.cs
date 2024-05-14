namespace AwesomeCompiler.RegularExpressions;

public class Sequence : Node
{
    public List<Node> Children { get; set; } = [];

    public void Add(Node node) => Children.Add(node);

    public Node PopLast()
    {
        var node = Children.Last();
        Children.Remove(node);
        return node;
    }
}
