using System.Diagnostics;

namespace Core.LexicalAnalysis;

[DebuggerDisplay("{Type} {Value}")]
public class Token<T>(T type)
{
    public T Type { get; init; } = type;
    public string Value { get; set; } = string.Empty;
}
