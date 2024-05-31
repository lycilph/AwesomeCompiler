namespace AwesomeCompilerCore.Common;

public class CharSet : IEquatable<CharSet>
{
    public bool IsNegative { get; set; }
    public HashSet<char> Chars { get; } = [];
    public string Label { get; set; } = string.Empty;

    public CharSet() { }
    public CharSet(char c)
    {
        Add(c);
    }
    public CharSet(char s, char e)
    {
        Add(s, e);
    }

    public void Add(char c)
    {
        Chars.Add(c);
        Label += c.CharToString();
    }

    public void Add(char s, char e)
    {
        for (char c = s; c <= e; c++)
            Chars.Add(c);
        Label += $"{s.CharToString()}-{e.CharToString()}";
    }

    public HashSet<char> Get()
    {
        if (IsNegative)
        {
            // Remove Chars from All
            var result = All();
            foreach (var c in Chars)
                result.Chars.Remove(c);
            return result.Chars;
        }
        else
            return Chars;
    }

    public static CharSet All() => new((char)0, (char)127);

    public override string ToString() => $"[{(IsNegative ? "^" : "")}{Label}]";

    #region Equals
    public bool Equals(CharSet? other)
    {
        if (other is null)
            return false;

        return IsNegative == other.IsNegative &&
               Chars.SetEquals(other.Chars);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CharSet);
    }

    public static bool operator ==(CharSet left, CharSet right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if ((object)left == null || (object)right == null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(CharSet left, CharSet right)
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
            hash = (hash * 16777619) ^ IsNegative.GetHashCode();
            foreach (var c in Chars)
                hash = (hash * 16777619) ^ c.GetHashCode();
            return hash;
        }
    }
    #endregion
}
