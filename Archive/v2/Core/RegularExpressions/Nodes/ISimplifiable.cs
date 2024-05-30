namespace Core.RegularExpressions.Nodes;

public interface ISimplifiable
{
    public ISimplifiable? Parent { get; set; }
    public void Replace(RegexNode oldNode, RegexNode newNode);
}
