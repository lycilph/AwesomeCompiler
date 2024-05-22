using System.Diagnostics;

namespace Core.Graphs;

[DebuggerDisplay("Id={Id}, {Transitions.Count} transitions, IsFinal={IsFinal}")]
public class Node(bool final = false)
{
    private static int id_counter = 0;

    public int Id { get; set; } = id_counter++;
    public bool IsFinal { get; set; } = final;
    public List<Transition> Transitions { get; set; } = [];

    public void AddEpsilonTransition(Node to) => Transitions.Add(new Transition(to, new Symbol(epsilon: true, any: false)));
    public void AddAnyTransition(Node to) => Transitions.Add(new Transition(to, new Symbol(epsilon: false, any: true)));
    public void AddTransition(Node to, Symbol s) => Transitions.Add(new Transition(to, s));
}
