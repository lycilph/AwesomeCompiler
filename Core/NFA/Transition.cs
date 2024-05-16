using System.Diagnostics;

namespace Core.NFA;

[DebuggerDisplay("-> {To} {Symbol}")]
public class Transition<T>(T to, Symbol symbol)
{
    public Symbol Symbol { get; set; } = symbol;
    public T To { get; set; } = to;
}
