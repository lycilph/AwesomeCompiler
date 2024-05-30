using Core.RegularExpressions;
using System.Diagnostics;

namespace Core.LexicalAnalysis;

[DebuggerDisplay("{Regex} {Type} (skip={Skip})")]
public class Rule<T>(Regex regex, T type, bool skip = false)
{
    public Regex Regex { get; init; } = regex;
    public T Type { get; init; } = type;
    public bool Skip { get; init; } = skip;
}
