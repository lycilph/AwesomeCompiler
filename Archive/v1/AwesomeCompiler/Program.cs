using Core.NFA;
using Core.NFA.Algorithms;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace AwesomeCompiler;

internal class Program
{
    private const string dotInput = "graph.txt";

    private static void GenerateDotGraph(Node start, string filename)
    {
        var dotGraph = DotGraphGenerator.Generate(start);
        File.WriteAllText(dotInput, dotGraph);

        var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "dot.exe",
            Arguments = $"{dotInput} -Tpng -o{filename}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.StartInfo = startInfo;
        process.Start();

        Console.WriteLine("std out: " + process.StandardOutput.ReadToEnd());
        Console.WriteLine("std err: " + process.StandardError.ReadToEnd());

        process.WaitForExit();
    }

    static void Main()
    {
        var number = new Regex("[0-9](\\.[0-9]+)?");
        var identifier = new Regex("[a-z]([a-z]|[0-9])*");
        var str = new Regex("\"[a-z]*\"");

        // Simplify the regular expressions
        var simplifier = new SimplifyVisitor();
        number.GetRoot().Accept(simplifier);
        identifier.GetRoot().Accept(simplifier);
        str.GetRoot().Accept(simplifier);

        // Convert to nfa
        var numberNFA = number.GetRoot().ConvertToNFA();
        var identifierNFA = identifier.GetRoot().ConvertToNFA();
        var strNFA = str.GetRoot().ConvertToNFA();

        // Combine NFAs
        var combinedNFA = new Graph();
        combinedNFA.Start = new Node();
        combinedNFA.Start.AddEmptyTransition(numberNFA.Start);
        combinedNFA.Start.AddEmptyTransition(identifierNFA.Start);
        combinedNFA.Start.AddEmptyTransition(strNFA.Start);

        // Generate dot graphs for individual NFAs
        GenerateDotGraph(numberNFA.Start, "NumberNFA.png");
        GenerateDotGraph(identifierNFA.Start, "IdentifierNFA.png");
        GenerateDotGraph(strNFA.Start, "StrNFA.png");
        GenerateDotGraph(combinedNFA.Start, "CombinedNFA.png");

        var sc = new SubsetConstruction();
        var dfa = sc.Execute(combinedNFA);
        GenerateDotGraph(dfa, "dfa.png");

        var sm = new StateMinimization();
        var minimizedDFA = sm.Execute(dfa);
        GenerateDotGraph(minimizedDFA, "minimized_dfa.png");

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
