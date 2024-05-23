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
        Label = c.ToString();
    }

    public void Add(char c)
    {
        Chars.Add(c);
        Label += c.ToString();
    }

    public void Add(char s, char e)
    {
        for (char c = s; c != e; c++)
            Chars.Add(c);
        Label += $"{s}-{e}";
    }

    public override string ToString() => (IsNegative?"^":"") + Label;

    public bool Equals(CharacterSet? other)
    {
        if (other is null)
            return false;

        return IsNegative == other.IsNegative &&
               Chars.SequenceEqual(other.Chars);
    }

    public override bool Equals(object obj)
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
}
