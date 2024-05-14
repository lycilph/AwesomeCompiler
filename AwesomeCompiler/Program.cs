using System.Text.RegularExpressions;

namespace AwesomeCompiler;

internal class Program
{
    static void Main()
    {
        //var source = File.ReadAllText("Input\\lox.grammar");
        //Console.WriteLine(source);

        //var pattern = @"[0-9]+(.[0-9]+(g*))?";
        //var re = new Regex(pattern);

        var match1 = Regex.IsMatch("ab", "abc|d");
        var match2 = Regex.IsMatch("abc", "abc|d");
        var match3 = Regex.IsMatch("abd", "abc|d");
        var match4 = Regex.IsMatch("abq", "abc|d");
        var match5 = Regex.IsMatch("abdd", "abc|d");

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
