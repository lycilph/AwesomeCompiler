using AwesomeCompilerCore.RegularExpressions;

namespace AwesomeCompilerSandbox;

internal class Program
{
    static void Main()
    {
        try
        {
            var str = "abd*";
            var input = "abdddd";
            var regex = new Regex(str);

            Console.WriteLine($"{str} match {input} = {regex.Match(input)}");
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
