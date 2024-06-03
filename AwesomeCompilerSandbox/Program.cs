using AwesomeCompilerCore.Graphs.NFAAlgorithms;
using AwesomeCompilerCore.RegularExpressions;

namespace AwesomeCompilerSandbox;

internal class Program
{
    static void Main()
    {
        try
        {
            var regex = new Regex("(a|b)*.+c?");
            var nfa = RegexToNFAVisitor.Run(regex);
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
