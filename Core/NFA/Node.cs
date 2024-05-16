using System.Diagnostics;

namespace Core.NFA;

[DebuggerDisplay("Node ({Id})")]
public class Node
{
    private static int IdCounter = 0;

    public int Id { get; set; } = 0;
    public bool IsFinal { get; set; } = false;
    public List<Transition<Node>> Transitions { get; set; } = [];

    public Node(bool final = false)
    {
        Id = IdCounter++;
        IsFinal = final;
    }

    public void AddEmptyTransition(Node to) => AddTransition(to, new Symbol() { isEpsilon = true, label = "Epsilon" });

    public void AddAnyTransition(Node to) => AddTransition(to, new Symbol() { isAny = true, label = "Any" });

    public void AddTransition(Node to, HashSet<char> chars, string label) => AddTransition(to, new Symbol() { chars = chars, label = label });

    public void AddTransition(Node to, Symbol symbol)
    {
        Transitions.Add(new Transition<Node>(to, symbol));
    }
}
