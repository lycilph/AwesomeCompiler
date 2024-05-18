using Core.NFA.Algorithms;
using System.Diagnostics;

namespace Core.NFA;

[DebuggerDisplay("-> {To} {Symbol}")]
public class Transition<T>(T to, Symbol symbol)
{
    private static readonly SymbolComparer comparer = new();

    public Symbol Symbol { get; set; } = symbol;
    public T To { get; set; } = to;

    public bool IsEpsilon() => Symbol.isEpsilon;

    public bool Match(Symbol s) => comparer.Equals(Symbol, s);
}
