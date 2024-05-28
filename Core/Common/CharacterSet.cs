namespace Core.Common;

public class CharacterSet : IEquatable<CharacterSet>
{
    public bool IsNegative { get; set; }
    public HashSet<char> Chars { get; } = [];
    public string Label { get; set; } = string.Empty;

    public CharacterSet() {}
    public CharacterSet(char c)
    {
        Chars.Add(c);
        Label = c.CharToString();
    }
    public CharacterSet(char s, char e)
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

    public HashSet<char> GetCharSet()
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

    public override string ToString() => $"[{(IsNegative?"^":"")}{Label}]";

    public bool Equals(CharacterSet? other)
    {
        if (other is null)
            return false;

        return IsNegative == other.IsNegative &&
               Chars.SequenceEqual(other.Chars);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CharacterSet);
    }

    public override int GetHashCode()
    {
        if (Chars.Count == 0)
            return 0;

        int hash = 17;
        hash = hash * 23 + IsNegative.GetHashCode();
        foreach (var c in Chars)
            hash = hash ^ HashCode.Combine(c);
        return hash;
    }

    public static CharacterSet All() => new CharacterSet((char)0, (char)127);
}
