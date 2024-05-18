namespace Core.RegularExpressions.Algorithms;

public interface IVisitor
{
    void Visit(CharacterNode node);
    void Visit(AlternationNode node);
    void Visit(ConcatenationNode node);
    void Visit(StarNode node);
    void Visit(PlusNode node);
    void Visit(OptionalNode node);
}
