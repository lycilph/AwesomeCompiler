using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Nodes;

namespace AwesomeCompilerSandbox;

internal class Program
{
    static void Main()
    {
        try
        {
            var str = "a?|b+(c*.)[^0-9]";
            var regex1 = new Regex(str);

            RegexNode.ResetIdCounter();
            var regex2 = new Regex(str);

            Console.WriteLine($"regex1 = regex2 => {regex1 == regex2}, (hashes {regex1.GetHashCode()}, {regex2.GetHashCode()})");
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
