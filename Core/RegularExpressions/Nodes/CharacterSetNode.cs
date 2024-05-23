using Core.Common;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Character set node [{ToString()}]")]
public class CharacterSetNode : RegexNode, IEquatable<CharacterSetNode>
{
    public bool IsNegative { get; }
    public List<CharacterSetElement> Elements { get; } = [];

    public CharacterSetNode(bool negate)
    {
        IsNegative = negate;
    }
    public CharacterSetNode(char c)
    {
        Add(c);
    }
    public CharacterSetNode(char s, char e, bool negate = false)
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

    public CharacterSet GetCharacterSet()
    {
        var set = new CharacterSet() {  IsNegative = IsNegative };

        foreach (var element in Elements)
        {
            switch (element)
            {
                case SingleCharacterSetElement s:
                    set.Add(s.Value);
                    break;
                case RangeCharacterSetElement r:
                    set.Add(r.Start, r.End);
                    break;
                default:
                    throw new InvalidOperationException();
            };
        }

        return set;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode) { }

    public bool Equals(CharacterSetNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               IsNegative == other.IsNegative &&
               Elements.SequenceEqual(other.Elements);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterSetNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + IsNegative.GetHashCode();
        foreach (var e in Elements)
            hash = hash * 23 + e.GetHashCode();
        return hash;
    }
}
