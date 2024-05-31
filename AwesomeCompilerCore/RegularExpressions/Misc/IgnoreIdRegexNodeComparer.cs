using AwesomeCompilerCore.RegularExpressions.Nodes;
using System.Diagnostics.CodeAnalysis;

namespace AwesomeCompilerCore.RegularExpressions.Misc;

public class IgnoreIdRegexNodeComparer : IEqualityComparer<RegexNode>
{
    public bool Equals(RegexNode? x, RegexNode? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x == null || y == null)
            return false;

        if (x.GetType() != y.GetType())
            return false;

        if (x is Regex re1 && y is Regex re2)
            return re1.Pattern == re2.Pattern &&
                   Equals(re1.Root, re2.Root);

        if (x is AnyCharacterRegexNode && y is AnyCharacterRegexNode)
            return true;

        if (x is CharacterRegexNode char1 && y is CharacterRegexNode char2)
            return char1.Value == char2.Value;

        if (x is CharacterSetRegexNode set1 && y is CharacterSetRegexNode set2)
            return set1.IsNegative == set2.IsNegative &&
                   set1.Elements.SequenceEqual(set2.Elements);

        if (x is AlternationRegexNode alt1 && y is AlternationRegexNode alt2)
            return Equals(alt1.Left, alt2.Left) &&
                   Equals(alt1.Right, alt2.Right);

        if (x is ConcatenationRegexNode cat1 && y is ConcatenationRegexNode cat2)
            return Equals(cat1.Left, cat2.Left) &&
                   Equals(cat1.Right, cat2.Right);

        if (x is StarRegexNode star1 && y is StarRegexNode star2)
            return Equals(star1.Child, star2.Child);

        if (x is PlusRegexNode plus1 && y is PlusRegexNode plus2)
            return Equals(plus1.Child, plus2.Child);

        if (x is OptionalRegexNode opt1 && y is OptionalRegexNode opt2)
            return Equals(opt1.Child, opt2.Child);

        return false;
    }

    public int GetHashCode([DisallowNull] RegexNode obj)
    {
        throw new NotImplementedException();
    }
}