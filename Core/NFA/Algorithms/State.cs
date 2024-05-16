using System.Diagnostics;

namespace Core.NFA.Algorithms;

[DebuggerDisplay("{id.ToString(),nq} - [{Label()}]")]
public class State
{
    private static int Counter = 0;

    public char id = (char)(65 + Counter++);
    public HashSet<Node> nodes = [];
    public List<Transition<State>> Transitions { get; set; } = [];

    public void Add(Node node) => nodes.Add(node);

    public void AddTransition(State to, char c)
    {
        Transitions.Add(Transition<State>.Create(this, to, c));
    }

    private string Label()
    {
        return string.Join(",", nodes.Select(n => n.Id).Order().ToList());
    }
}
