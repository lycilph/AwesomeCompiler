namespace Core.RegularExpressions.Algorithms;

public class SimplifyVisitor : IVisitor
{
    public void Visit(AlternationNode alternation) {}

    public void Visit(CharacterSetNode characterSet) {}

    public void Visit(SequenceNode sequence)
    {
        foreach (var nodes in sequence)
    }

    public void Visit(GroupNode group) {}

    public void Visit(MatchAnyCharacterNode matchAny) {}

    public void Visit(MatchSingleCharacterNode matchSingleCharacter) {}

    public void Visit(OptionalNode optional)
    {
        optional.Child = Simplify(optional.Child!);
    }

    public void Visit(PlusNode plus)
    {
        plus.Child = Simplify(plus.Child!);
    }

    public void Visit(StarNode star)
    {
        star.Child = Simplify(star.Child!);
    }

    private static Node Simplify(Node node)
    {
        var result = node;

        var done = false;
        while (!done)
        {
            switch (result)
            {
                case GroupNode group:
                    result = group.Child;
                    break;
                case SequenceNode sequence:
                    if (sequence.Count() == 1)
                        result = sequence.Last();
                    else
                        done = true;
                    break;
                case AlternationNode alternation:
                    if (alternation.Left is CharacterSetNode leftSet &&
                        alternation.Right is CharacterSetNode rightSet &&
                        leftSet.IsNegativeSet == rightSet.IsNegativeSet)
                    {
                        leftSet.AddSet(rightSet);
                        result = leftSet;
                    }
                    else
                        done = true;
                    break;
                default:
                    done = true;
                    break;
            }
        }
        

        return result;
    }
}
