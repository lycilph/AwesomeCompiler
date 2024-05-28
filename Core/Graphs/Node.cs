using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("Id={Id}, IsFinal={IsFinal}, Transitions to: {GetTransions()}")]
public class Node(bool final = false)
{
    private static int id_counter = 0;

    public int Id { get; set; } = id_counter++;
    public bool IsFinal { get; set; } = final;
    public List<Transition> Transitions { get; set; } = [];

    public string Rule { get; set; } = string.Empty; // This is used to mark final states with the "rule" that it accepts
    public HashSet<Node> Nodes { get; set; } = []; // This is used for storage for dfa generation (amongst other things)

    private string GetTransions() => string.Join(",", Transitions.Select(t => t.To.Id));

    public void AddEpsilonTransition(Node to) => Transitions.Add(new Transition(to, new Symbol(epsilon: true, any: false)));
    public void AddAnyTransition(Node to) => Transitions.Add(new Transition(to, new Symbol(epsilon: false, any: true)));
    public void AddTransition(Node to, Symbol s) => Transitions.Add(new Transition(to, s));
}
