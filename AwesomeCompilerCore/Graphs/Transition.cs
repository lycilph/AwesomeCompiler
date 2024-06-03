using System.Diagnostics;

namespace AwesomeCompilerCore.Graphs;

[DebuggerDisplay("-> {To.Id} {Symbol}")]
public class Transition(GraphNode to, Symbol symbol)
{
    public GraphNode To { get; set; } = to;
    public Symbol Symbol { get; set; } = symbol;
}