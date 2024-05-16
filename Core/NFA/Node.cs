using System.Diagnostics;

namespace Core.NFA;

[DebuggerDisplay("Node ({Id})")]
public class Node
{
    private static int IdCounter = 0;

    public int Id { get; set; } = 0;
    public bool IsFinal { get; set; } = false;
    public List<Transition> Transitions { get; set; } = [];

    public Node(bool final = false)
    {
        Id = IdCounter++;
        IsFinal = final;
    }

    public void AddTransition(Node to, HashSet<char> chars, string label = "")
    {
        Transitions.Add(Transition.Create(this, to, chars, label));
    }

    public void AddTransition(Node to, char c)
    {
        Transitions.Add(Transition.Create(this, to, c));
    }

    public void AddEmptyTransition(Node to)
    {
        Transitions.Add(Transition.CreateEmpty(this, to));
    }

    public void AddMatchAnyTransition(Node to)
    {
        Transitions.Add(Transition.CreateMatchAny(this, to));
    }
}
