using AwesomeCompilerCore.RegularExpressions.Visitors;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

public abstract class RegexNode
{
    private static int id_counter = 0;

    public int Id { get; } = id_counter++;
    public RegexNode? Parent { get; set; }

    public abstract bool Match(List<char> input);

    public abstract void Accept(IVisitor visitor);
    public abstract R Accept<R>(IVisitor<R> visitor);

    public static void ResetIdCounter() => id_counter = 0;
}
