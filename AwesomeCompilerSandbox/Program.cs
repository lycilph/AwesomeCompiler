using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Visitors;

namespace AwesomeCompilerSandbox;

internal class Program
{
    static void Main()
    {
        try
        {
            var str = "a?|b+(c*.)[0-9]";
            var tokens = RegexTokenizer.Tokenize(str);
            var node = RegexParser.Parse(tokens);
            ((Regex)node).Pattern = str;

            Console.WriteLine(PrintVisitor.Run(node));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Wait();
    }

    private static void Wait()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
