using Core.Common;
using Core.Graphs;
using Core.Graphs.Algorithms;
using Core.LexicalAnalysis;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using System.Text.Json;

namespace AwesomeCompilerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            //var str = File.ReadAllText(@"TestInput\TestGrammar.txt");
            //var gammar = new Grammar();
            //var tokens = gammar.Tokenize(str, verbose_output: false);
            //foreach (var token in tokens)
            //    Console.WriteLine($"Found token: {token}");

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static void LexingWithBacktracking()
    {
        var lexer_generator = new LexerGenerator();
        lexer_generator.Add(new Regex("abc|(abc)*d"), "Test");

        var lexer = lexer_generator.Generate();
        var str = "abcabcabcd";
        lexer.Run(str, verbose_output: true);
        var tokens = lexer.Run(str);
        foreach (var token in tokens)
            Console.WriteLine($"Found token: {token}");

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static void FullLexerExample()
    {
        var lexer_generator = new LexerGenerator();
        lexer_generator.Add(new Regex(@"[0-9](\.[0-9]+)?"), "Number");
        lexer_generator.Add(new Regex(@"[a-zA-Z_]([a-z_]|[A-Z]|[0-9])*"), "Identifier");
        lexer_generator.Add(new Regex("\"[^\"]*\""), "String");
        lexer_generator.Add(new Regex("[ \n\r\t]+"), "Whitespace", skip: true);
        lexer_generator.Add(new Regex(@"//[^\n]*\n"), "Comment", skip: true);
        lexer_generator.Add(new Regex("{"), "LeftBracket");
        lexer_generator.Add(new Regex("}"), "RightBracket");
        lexer_generator.Add(new Regex(@"\("), "leftParenthesis");
        lexer_generator.Add(new Regex(@"\)"), "RightParenthesis");
        lexer_generator.Add(new Regex(";"), "LineTerminator");
        lexer_generator.Add(new Regex(@"\+"), "Plus");
        lexer_generator.Add(new Regex("="), "Equal");

        var lexer = lexer_generator.Generate();
        var str = File.ReadAllText(@"TestInput\SimpleProg1.txt");
        var tokens = lexer.Run(str);
        foreach (var token in tokens)
            Console.WriteLine($"Found token: {token}");

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ReadTransitionTableFromFile()
    {
        var str = File.ReadAllText("transition_table.txt");
        var transition_table = JsonSerializer.Deserialize<Dictionary<int, Dictionary<char, int>>>(str);

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static void WriteTransitionTableToFile()
    {
        Dictionary<int, Dictionary<char, int>> transition_table = [];

        transition_table.Add(0, new Dictionary<char, int>());
        transition_table.Add(1, new Dictionary<char, int>());
        transition_table.Add(2, new Dictionary<char, int>());

        transition_table[0]['a'] = 1;
        transition_table[0]['b'] = 2;

        transition_table[1]['a'] = 0;
        transition_table[1]['b'] = -1;

        transition_table[2]['a'] = -1;
        transition_table[2]['b'] = -1;

        var str = JsonSerializer.Serialize(transition_table);
        Console.WriteLine(str);
        File.WriteAllText("transition_table.txt", str);
    }

    private static void ReadFile()
    {
        var path = @"TestInput\SimpleProg1.txt";

        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (var streamReader = new StreamReader(fileStream))
        using (var reader = new PushbackReader(streamReader))
        {
            int i;
            while ((i = reader.Read()) != -1)
            {
                Console.Write((char)i);
            }
        }

        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static void Lexer()
    {
        var regexSimplifier = new SimplifyVisitor();

        var numberRegex = new Regex(@"[0-9](\.[0-9]+)?");
        numberRegex.Node.Accept(regexSimplifier);
        RenderDotGraph("number_regex.png", numberRegex);

        var identifierRegex = new Regex("[a-zA-Z_]([a-z_]|[A-Z]|[0-9])*");
        identifierRegex.Node.Accept(regexSimplifier);
        RenderDotGraph("identifier_regex.png", identifierRegex);

        var stringRegex = new Regex("\"[^\"]*\"");
        stringRegex.Node.Accept(regexSimplifier);
        RenderDotGraph("string_regex.png", stringRegex);

        var whitespaceRegex = new Regex("[ \n\r\t]+");
        whitespaceRegex.Node.Accept(regexSimplifier);
        RenderDotGraph("whitespace_regex.png", whitespaceRegex);

        var numberNFA = RegexToNFAVisitor.Accept(numberRegex);
        numberNFA.End.First().Rule = "Number";
        RenderDotGraph("number_nfa.png", numberNFA.Start);

        var identifierNFA = RegexToNFAVisitor.Accept(identifierRegex);
        identifierNFA.End.First().Rule = "Identifier";
        RenderDotGraph("identifier_nfa.png", identifierNFA.Start);

        var stringNFA = RegexToNFAVisitor.Accept(stringRegex);
        stringNFA.End.First().Rule = "String";
        RenderDotGraph("string_nfa.png", stringNFA.Start);

        var whitespaceNFA = RegexToNFAVisitor.Accept(whitespaceRegex);
        whitespaceNFA.End.First().Rule = "Whitespace";
        RenderDotGraph("whitespace_nfa.png", whitespaceNFA.Start);

        var nfa = Graph.Combine([numberNFA, identifierNFA, stringNFA, whitespaceNFA]);
        RenderDotGraph("combined_nfa.png", nfa.Start);

        var dfa = NFAToDFACreator.Run(nfa.Start);
        RenderDotGraph("dfa.png", dfa);

        var minimized = DFAStateMinimizer.Run(dfa);
        RenderDotGraph("minimized_dfa.png", minimized);
    }

    private static void RenderDotGraph(string filename, Regex re)
    {
        var graph = DotGraphVisitor.Generate(re);
        DotWrapper.Render(filename, graph);
    }

    private static void RenderDotGraph(string filename, Node n)
    {
        var graph = DotGraphNodeWalker.Generate(n);
        DotWrapper.Render(filename, graph);
    }

    private static void Test1()
    {
        var str = @"[a-z_]([a-z_]|[A-Z]|[0-9])*";
        //var str = @"(a|b)*abb";
        var regex = new Regex(str);

        Console.WriteLine($"Input {str}");
        Console.WriteLine();

        var regexDotGraph = DotGraphVisitor.Generate(regex);
        DotWrapper.Render("regex.png", regexDotGraph);

        var nfa = RegexToNFAVisitor.Accept(regex);

        var nfaDotGraph = DotGraphNodeWalker.Generate(nfa.Start);
        DotWrapper.Render("nfa.png", nfaDotGraph);

        var dfaCreator = new NFAToDFACreator();
        var dfa = dfaCreator.Execute(nfa.Start);

        var dfaDotGraph = DotGraphNodeWalker.Generate(dfa);
        DotWrapper.Render("dfa.png", dfaDotGraph);

        var dfaStateMinizser = new DFAStateMinimizer();
        var minimizedDFA = dfaStateMinizser.Execute(dfa);

        var minimizedDfaDotGraph = DotGraphNodeWalker.Generate(minimizedDFA);
        DotWrapper.Render("minimized_dfa.png", minimizedDfaDotGraph);
    }
}
