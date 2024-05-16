namespace Core.RegularExpressions.Algorithms;

public class SimplifyVisitor : IVisitor
{
    public void Visit(AlternationNode alternation)
    {
        if (alternation.Parent == null)
            return;

        if (alternation.Left is CharacterSetNode leftSet &&
            alternation.Right is CharacterSetNode rightSet &&
            leftSet.IsNegativeSet == rightSet.IsNegativeSet)
        {
            leftSet.AddSet(rightSet);
            alternation.Parent.ReplaceNode(alternation, leftSet);
        }
        else if ((alternation.Left is MatchAnyCharacterNode && alternation.Right is CharacterSetNode) ||
                 (alternation.Right is MatchAnyCharacterNode && alternation.Left is CharacterSetNode))
        {
            alternation.Parent.ReplaceNode(alternation, new MatchAnyCharacterNode());
        }     
    }

    public void Visit(CharacterSetNode characterSet) {}

    public void Visit(SequenceNode sequence)
    {
        if (sequence.Parent != null && sequence.Count() == 1)
            sequence.Parent.ReplaceNode(sequence, sequence.Last());
    }

    public void Visit(GroupNode group)
    {
        if (group.Parent != null)
            group.Parent.ReplaceNode(group, group.Child);
    }

    public void Visit(MatchAnyCharacterNode matchAny) {}

    public void Visit(MatchSingleCharacterNode matchSingleCharacter) 
    {
        if (matchSingleCharacter.Parent != null)
            matchSingleCharacter.Parent.ReplaceNode(matchSingleCharacter, new CharacterSetNode(matchSingleCharacter.Value));
    }

    public void Visit(OptionalNode optional) {}

    public void Visit(PlusNode plus) {}

    public void Visit(StarNode star) {}
}
