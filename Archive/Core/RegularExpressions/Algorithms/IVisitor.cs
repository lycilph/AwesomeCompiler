namespace Core.RegularExpressions.Algorithms;

public interface IVisitor
{
    void Visit(AlternationNode alternation);
    void Visit(CharacterSetNode characterSet);
    void Visit(SequenceNode sequence);
    void Visit(GroupNode group);
    void Visit(MatchAnyCharacterNode matchAny);
    void Visit(MatchSingleCharacterNode matchSingleCharacter);
    void Visit(OptionalNode optional);
    void Visit(PlusNode plus);
    void Visit(StarNode star);
}
