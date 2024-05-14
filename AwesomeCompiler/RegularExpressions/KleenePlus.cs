namespace AwesomeCompiler.RegularExpressions;

public class KleenePlus : Node
{
    private Node node;

    public KleenePlus(Node node)
    {
        this.node = node;
    }
}
