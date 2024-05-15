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
        else if (alternation.Left is MatchSingleCharacterNode singleLeft &&
                 alternation.Right is MatchSingleCharacterNode singleRight)
        {
            var set = new CharacterSetNode();
            set.AddCharacter(singleLeft.Value);
            set.AddCharacter(singleRight.Value);

            alternation.Parent.ReplaceNode(alternation, set);
        }
        else if (alternation.Left is MatchAnyCharacterNode anyCharacterLeft &&
                (alternation.Right is MatchSingleCharacterNode ||
                 alternation.Right is CharacterSetNode))
        {
            alternation.Parent.ReplaceNode(alternation, anyCharacterLeft);
        }
        else if (alternation.Right is MatchAnyCharacterNode anyCharacterRight &&
                (alternation.Left is MatchSingleCharacterNode ||
                 alternation.Left is CharacterSetNode))
        {
            alternation.Parent.ReplaceNode(alternation, anyCharacterRight);
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

    public void Visit(MatchSingleCharacterNode matchSingleCharacter) {}

    public void Visit(OptionalNode optional) {}

    public void Visit(PlusNode plus) {}

    public void Visit(StarNode star) {}
}
