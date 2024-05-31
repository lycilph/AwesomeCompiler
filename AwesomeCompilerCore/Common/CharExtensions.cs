namespace AwesomeCompilerCore.Common;

public static class CharExtensions
{
    public static string CharToString(this char c)
    {
        return c switch
        {
            ' ' => "WS",
            '\"' => @"\""",
            '\\' => @"\\",
            '\n' => @"\\n",
            '\r' => @"\\r",
            '\t' => @"\\t",
            _ => c.ToString(),
        };
    }
}