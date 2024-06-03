using System.Diagnostics;

namespace AwesomeCompilerCore.Graphs;

[DebuggerDisplay("Id={Id}, IsFinal={IsFinal}, Transitions to: {GetTransionsAsString()}")]
public class GraphNode(bool final = false)
{
    private static int id_counter = 0;

    public int Id { get; set; } = id_counter++;
    public bool IsFinal { get; set; } = final;
    public List<Transition> Transitions { get; set; } = [];
    public Dictionary<string, object> Attributes { get; set; } = [];

    private string GetTransionsAsString() => string.Join(",", Transitions.Select(t => t.To.Id));

    public void AddEpsilonTransition(GraphNode to) => Transitions.Add(new Transition(to, Symbol.Epsilon()));
    public void AddAnyTransition(GraphNode to) => Transitions.Add(new Transition(to, Symbol.Any()));
    public void AddTransition(GraphNode to, Symbol s) => Transitions.Add(new Transition(to, s));

    public T Get<T>(string attribute) => (T)Attributes[attribute];
}
