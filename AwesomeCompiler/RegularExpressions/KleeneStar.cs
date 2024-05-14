namespace AwesomeCompiler.RegularExpressions;

public class KleeneStar : Node
{
    private Node node;

    public KleeneStar(Node node)
    {
        this.node = node;
    }
}
