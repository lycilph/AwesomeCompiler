using Core.RegularExpressions.Algorithms;

namespace Core.RegularExpressions.Nodes;

public abstract class RegexNode : ISimplifiable
{
    private static int id_counter = 0;

    public int Id { get; } = id_counter++;
    public ISimplifiable? Parent { get; set; }

    public abstract void Accept(IVisitor visitor);
    public abstract R Accept<R>(IVisitor<R> visitor);

    public abstract void Replace(RegexNode oldNode, RegexNode newNode);
}
