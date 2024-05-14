namespace AwesomeCompiler.RegularExpressions;

public class CharacterSet : Node
{
    private readonly string set;

    public CharacterSet(string set)
    {
        this.set = set;
    }
}
