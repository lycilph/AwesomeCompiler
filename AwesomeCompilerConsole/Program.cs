using Core;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;

namespace AwesomeCompilerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var str = "(a|b)*ab+b?";
            Console.WriteLine($"Input {str}");
            
            var tokenizer = new RegexTokenizer(str);
            var tokens = tokenizer.Tokenize();
            tokens.ForEach(Console.WriteLine);

            var parser = new RegexParser(tokens);
            var node = parser.Parse();
            var printVisitor = new PrintAstVisitor();
            node.Accept(printVisitor);

            var dotGraph = DotGraphVisitor.Generate(node);
            Console.WriteLine(dotGraph);

            DotWrapper.Render("graph.png", dotGraph);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
