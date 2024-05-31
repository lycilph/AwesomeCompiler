using AwesomeCompilerCore.RegularExpressions.Visitors;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

public class CharacterSetRegexNode : RegexNode, IEquatable<CharacterSetRegexNode>
{
    public bool IsNegative { get; }
    public List<CharacterSetElement> Elements { get; } = [];

    public CharacterSetRegexNode(bool negate)
    {
        IsNegative = negate;
    }
    public CharacterSetRegexNode(char c)
    {
        Add(c);
    }
    public CharacterSetRegexNode(char s, char e, bool negate = false)
    {
        IsNegative = negate;
        Add(s, e);
    }

    public void Add(CharacterSetElement element) => Elements.Add(element);
    public void Add(char c) => Add(new SingleCharacterSetElement(c));
    public void Add(char s, char e) => Add(new RangeCharacterSetElement(s, e));
    
    public override string ToString()
    {
        var negate = IsNegative ? "^" : "";
        var elements = string.Join("", Elements.Select(e => e.ToString()));
        return negate + elements;
    }

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(CharacterSetRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               IsNegative == other.IsNegative &&
               Elements.SequenceEqual(other.Elements);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterSetRegexNode);
    }

    public static bool operator ==(CharacterSetRegexNode left, CharacterSetRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(CharacterSetRegexNode left, CharacterSetRegexNode right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        // From here: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
        unchecked // Overflow is fine, just wrap
        {
            int hash = (int)2166136261;
            // Suitable nullity checks etc, of course :)
            hash = (hash * 16777619) ^ Id.GetHashCode();
            hash = (hash * 16777619) ^ IsNegative.GetHashCode();
            foreach (var element in Elements)
                hash = (hash * 16777619) ^ element.GetHashCode();
            return hash;
        }
    }
    #endregion
}