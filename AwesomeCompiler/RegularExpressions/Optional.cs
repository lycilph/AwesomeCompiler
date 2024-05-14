namespace AwesomeCompiler.RegularExpressions;

public class Optional : Node
{
    private Node node;

    public Optional(Node node)
    {
        this.node = node;
    }
}
