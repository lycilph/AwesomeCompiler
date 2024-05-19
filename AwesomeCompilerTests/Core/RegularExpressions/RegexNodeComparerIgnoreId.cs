using Core.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace AwesomeCompilerTests.Core.RegularExpressions;

public class RegexNodeComparerIgnoreId : IEqualityComparer<RegexNode>
{
    public bool Equals(RegexNode? x, RegexNode? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x == null || y == null)
            return false;

        if (x.GetType() != y.GetType())
            return false;
        
        if (x is AnyCharacterNode && y is AnyCharacterNode)
            return true;

        if (x is CharacterNode char1 && y is CharacterNode char2)
            return char1.Value == char2.Value;

        if (x is CharacterSetNode set1 && y is CharacterSetNode set2)
            return set1.Negate == set2.Negate &&
                   set1.Elements.SequenceEqual(set2.Elements);

        if (x is AlternationNode alt1 && y is AlternationNode alt2)
            return Equals(alt1.Left, alt2.Left) &&
                   Equals(alt1.Right, alt2.Right);

        if (x is ConcatenationNode cat1 && y is ConcatenationNode cat2)
            return Equals(cat1.Left, cat2.Left) &&
                   Equals(cat1.Right, cat2.Right);

        if (x is StarNode star1 && y is StarNode star2)
            return Equals(star1.Child, star2.Child);

        if (x is PlusNode plus1 && y is PlusNode plus2)
            return Equals(plus1.Child, plus2.Child);

        if (x is OptionalNode opt1 && y is OptionalNode opt2)
            return Equals(opt1.Child, opt2.Child);

        return false;
    }

    public int GetHashCode([DisallowNull] RegexNode obj)
    {
        throw new NotImplementedException();
    }
}
