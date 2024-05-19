using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace AwesomeCompilerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var str = @"[a-z]|[0-9]";
            Console.WriteLine($"Input {str}");
            Console.WriteLine();
            
            var tokenizer = new RegexTokenizer(str);
            var tokens = tokenizer.Tokenize();
            Console.WriteLine("Tokens found:");
            tokens.ForEach(Console.WriteLine);
            Console.WriteLine();

            var parser = new RegexParser(tokens);
            var node = parser.Parse();
            var printVisitor = new PrintAstVisitor();
            Console.WriteLine("AST");
            node.Accept(printVisitor);
            Console.WriteLine();

            var simplifier = new SimplifyVisitor();
            node.Accept(simplifier);
            Console.WriteLine("Simplifed AST");
            node.Accept(printVisitor);
            Console.WriteLine();

            //var dotGraph = DotGraphVisitor.Generate(node);
            //Console.WriteLine(dotGraph);

            //DotWrapper.Render("graph.png", dotGraph);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
