using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("-> {To.Id} {Symbol}")]
public class Transition(Node to, Symbol symbol)
{
    public Node To { get; set; } = to;
    public Symbol Symbol { get; set; } = symbol;
}
