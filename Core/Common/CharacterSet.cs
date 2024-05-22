namespace Core.Common;

public class CharacterSet
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
}
