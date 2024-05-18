using Core.RegularExpressions;

namespace AwesomeCompilerConsole;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var str = "(a|b)*abb";
            var tokenizer = new RegexTokenizer(str);
            var tokens = tokenizer.Tokenize();
            var parser = new RegexParser(tokens);
            var node = parser.Parse();

            Console.WriteLine($"Input {str}");
            tokens.ForEach(Console.WriteLine);
            PrintAst(node);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static void PrintAst(RegexNode node, int indent = 0)
    {
        switch (node)
        {
            case CharacterNode characterNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name + " " + characterNode.Value);
                break;
            case AlternationNode alternationNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name);
                PrintAst(alternationNode.Left, indent + 2);
                PrintAst(alternationNode.Right, indent + 2);
                break;
            case ConcatenationNode concatenationNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name);
                PrintAst(concatenationNode.Left, indent + 2);
                PrintAst(concatenationNode.Right, indent + 2);
                break;
            case StarNode starNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name);
                PrintAst(starNode.Child, indent + 2);
                break;
            case PlusNode plusNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name);
                PrintAst(plusNode.Child, indent + 2);
                break;
            case OptionalNode optionalNode:
                Console.WriteLine(new string(' ', indent) + node.GetType().Name);
                PrintAst(optionalNode.Child, indent + 2);
                break;
            default:
                throw new InvalidDataException($"Unknown node type {node.GetType().Name}");
        }
    }
}
