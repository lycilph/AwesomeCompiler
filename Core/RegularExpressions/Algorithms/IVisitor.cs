namespace Core.RegularExpressions.Algorithms;

public interface IVisitor
{
    void Visit(CharacterNode node);
    void Visit(CharacterSetNode node);
    void Visit(AlternationNode node);
    void Visit(ConcatenationNode node);
    void Visit(StarNode node);
    void Visit(PlusNode node);
    void Visit(OptionalNode node);
}

public interface IVisitor<R>
{
    R Visit(CharacterNode node);
    R Visit(CharacterSetNode node);
    R Visit(AlternationNode node);
    R Visit(ConcatenationNode node);
    R Visit(StarNode node);
    R Visit(PlusNode node);
    R Visit(OptionalNode node);
}