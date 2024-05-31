using AwesomeCompilerCore.Common;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Diagnostics;

namespace AwesomeCompilerCore.RegularExpressions.Nodes;

[DebuggerDisplay("Character node [{ToString()}]")]
public class CharacterRegexNode(char value) : RegexNode, IEquatable<CharacterRegexNode>
{
    public char Value { get; } = value;

    public override string ToString() => Value.CharToString();

    #region Visitors
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);
    #endregion

    #region Equals
    public bool Equals(CharacterRegexNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterRegexNode);
    }
    
    public static bool operator ==(CharacterRegexNode left, CharacterRegexNode right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(CharacterRegexNode left, CharacterRegexNode right)
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
            hash = (hash * 16777619) ^ Value.GetHashCode();
            return hash;
        }
    }
    #endregion
}
