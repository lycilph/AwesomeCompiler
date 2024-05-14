using AwesomeCompiler.RegularExpressions;

namespace AwesomeCompiler;

internal class Program
{
    static void Main()
    {
        //var source = File.ReadAllText("Input\\lox.grammar");
        //Console.WriteLine(source);

        var pattern = @"[0-9]+(.[0-9]+(g*))?";
        var re = new Regex(pattern);

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
