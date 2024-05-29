using Core.Common;
using Core.Graphs;
using Core.Graphs.Algorithms;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace Core.LexicalAnalysis;

public class LexerGeneratorOld
{
    private readonly LexerOld lexer = new();
    private readonly List<Graph> nfas = [];
    private readonly SimplifyVisitor simplifier = new SimplifyVisitor();
    private readonly RegexToNFAVisitor nfa_visitor = new RegexToNFAVisitor();
    private readonly Dictionary<int, Dictionary<char, int>> transition_table = [];
    private readonly Dictionary<int, Tuple<string,bool>> accept_states = [];

    public LexerOld Generate()
    {
        Directory.CreateDirectory("Output");

        var combined_nfa = Graph.Combine(nfas);
        RenderDotGraph(@"Output\combined_nfa.png", combined_nfa.Start);
     
        var dfa = NFAToDFACreator.Run(combined_nfa.Start);
        RenderDotGraph(@"Output\dfa.png", dfa);

        var minimized_dfa = DFAStateMinimizer.Run(dfa);
        RenderDotGraph(@"Output\minimized_dfa.png", minimized_dfa);

        GenerateTables(minimized_dfa);
        lexer.Set(minimized_dfa.Id, transition_table, accept_states);

        return lexer;
    }

    public void Add(Regex regex, string rule, bool skip = false)
    {
        regex.Node.Accept(simplifier);
        RenderDotGraph(@"Output\"+rule+"_regex.png", regex);

        var nfa = regex.Node.Accept(nfa_visitor);
        nfa.End.First().Rule = rule;
        nfa.End.First().Skip = skip;
        RenderDotGraph(@"Output\"+rule+"_nfa.png", nfa.Start);

        nfas.Add(nfa);
    }

    private static void RenderDotGraph(string filename, Node n)
    {
        var graph = DotGraphNodeWalker.Generate(n);
        DotWrapper.Render(filename, graph);
    }

    private static void RenderDotGraph(string filename, Regex re)
    {
        var graph = DotGraphVisitor.Generate(re);
        DotWrapper.Render(filename, graph);
    }

    private void GenerateTables(Node node)
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

            if (n.IsFinal)
                accept_states[n.Id] = Tuple.Create(n.Rule, n.Skip);

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
        if (!transition_table.ContainsKey(from))
            transition_table[from] = new Dictionary<char, int>();

        transition_table[from][c] = to;
    }
}