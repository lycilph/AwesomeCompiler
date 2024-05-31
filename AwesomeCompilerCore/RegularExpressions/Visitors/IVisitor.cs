using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerCore.RegularExpressions.Visitors;

public interface IVisitor
{
    void Visit(Regex node);
    void Visit(AnyCharacterRegexNode node);
    void Visit(CharacterRegexNode node);
    void Visit(CharacterSetRegexNode node);
    void Visit(AlternationRegexNode node);
    void Visit(ConcatenationRegexNode node);
    void Visit(StarRegexNode node);
    void Visit(PlusRegexNode node);
    void Visit(OptionalRegexNode node);
}

public interface IVisitor<R>
{
    R Visit(Regex node);
    R Visit(AnyCharacterRegexNode node);
    R Visit(CharacterRegexNode node);
    R Visit(CharacterSetRegexNode node);
    R Visit(AlternationRegexNode node);
    R Visit(ConcatenationRegexNode node);
    R Visit(StarRegexNode node);
    R Visit(PlusRegexNode node);
    R Visit(OptionalRegexNode node);
}