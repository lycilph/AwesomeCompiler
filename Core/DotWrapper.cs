using System.Diagnostics;

namespace Core;

public static class DotWrapper
{
    private const string temp_filename = "graph.txt";

    public static void Render(string filename, string input)
    {
        File.WriteAllText(temp_filename, input);

        var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "dot.exe",
            Arguments = $"{temp_filename} -Tpng -o{filename}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();

        var stdOuput = process.StandardOutput.ReadToEnd();
        if (!string.IsNullOrEmpty(stdOuput))
            Console.WriteLine(stdOuput);

        var stdErr = process.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(stdErr))
            Console.WriteLine(stdErr);
    }
}
