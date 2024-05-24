using Core.Graphs;

namespace Core.LexicalAnalysis;

public class Lexer
{
    private readonly Dictionary<int, Dictionary<char, int>> transition_table = [];
    private readonly Dictionary<int, string> accept_states = [];
    private readonly int start_state;

    public Lexer(Node dfa)
    {
        start_state = dfa.Id;
        WalkTree(dfa);
    }

    private void WalkTree(Node node)
    {
        var visited = new HashSet<Node>();
        var toVisit = new Stack<Node>();

        toVisit.Push(node);

        while (toVisit.Count > 0)
        {
            var n = toVisit.Pop();
            if (visited.Contains(n))
                continue;

            visited.Add(n);

            foreach (var t in n.Transitions)
            {
                var chars = t.Symbol.GetCharSet();
                foreach (var c in chars)
                    Addtransition(n.Id, t.To.Id, c);
                
                if (!visited.Contains(t.To))
                    toVisit.Push(t.To);
            }
        }
    }

    private void Addtransition(int from, int to, char c)
    {
        Console.WriteLine($"Adding transition {from}->{to} on {c}");
    }
}
