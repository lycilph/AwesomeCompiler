using System.Diagnostics;

namespace Core.NFA.Algorithms;

[DebuggerDisplay("{id.ToString(),nq} - [{Label()}]")]
public class State
{
    private static int Counter = 0;

    public char id = (char)(65 + Counter++);
    public HashSet<Node> nodes = [];
    public bool IsFinal { get; set; } = false;
    public List<Transition<State>> Transitions { get; set; } = [];

    public void Add(Node node) => nodes.Add(node);

    public void AddTransition(State to, Symbol s) => Transitions.Add(new Transition<State>(to, s));

    public bool Contains(Node n) => nodes.Contains(n);

    private string Label()
    {
        return string.Join(",", nodes.Select(n => n.Id).Order().ToList());
    }
}
