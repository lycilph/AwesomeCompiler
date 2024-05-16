using System.Diagnostics;

namespace Core.NFA;

[DebuggerDisplay("{From} -> {To} {Label}")]
public class Transition<T>
{
    // An epsilon transition is an "empty" transition not consuming any input
    public bool Epsilon { get; set; } = false;
    public bool MatchAny { get; set; } = false;
    public HashSet<char> Chars { get; set; } = [];
    public T From { get; set; } = default!;
    public T To { get; set; } = default!;
    public string Label { get; set; } = string.Empty;

    public bool Match(char c) => MatchAny || Chars.Contains(c);

    public static Transition<T> CreateEmpty(T from, T to)
    {
        return new Transition<T>()
        {
            From = from,
            To = to,
            Epsilon = true
        };
    }

    public static Transition<T> CreateMatchAny(T from, T to)
    {
        return new Transition<T>()
        {
            From = from,
            To = to,
            MatchAny = true
        };
    }

    public static Transition<T> Create(T from, T to, char c)
    {
        var result = new Transition<T>()
        {
            From = from,
            To = to,
            Label = c.ToString()
        };
        result.Chars.Add(c);

        return result;
    }

    public static Transition<T> Create(T from, T to, HashSet<char> chars, string label = "")
    {
        var result = new Transition<T>()
        {
            From = from,
            To = to,
            Label = string.IsNullOrEmpty(label) ? string.Join("", chars) : label,
        };
        result.Chars.UnionWith(chars);

        return result;
    }
}
