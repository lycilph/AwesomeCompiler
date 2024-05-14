namespace AwesomeCompiler.RegularExpressions;

public class Regex
{
    private readonly Node root;

    public Regex(string pattern)
    {
        Console.WriteLine($"Pattern: {pattern}");

        var sequence = new Sequence();
        ParseSequence(pattern, sequence);
        root = sequence;
    }

    private int ParseSequence(string pattern, Sequence sequence)
    {
        var current = 0;
        while (current < pattern.Length)
        {
            char c = pattern[current];
            switch (c)
            {
                case '[':
                    {
                        var end = pattern.IndexOf(']', current);
                        if (end == -1) Console.WriteLine("Unmatched ] found");
                        
                        var set = ParseCharacterSet(pattern.Substring(current+1, end-current-1));
                        sequence.Add(set);

                        current = end + 1;
                    }
                    break;
                case '(':
                    {
                        int end = 0;
                        var node = ParseGroup(pattern[current..], ref end);
                        sequence.Add(node);
                        current += end + 1;
                    }
                    break;
                case ')':
                    return current+1;
                case '+':
                    {
                        Console.WriteLine("Found a kleene plus: +");
                        var node = sequence.PopLast();
                        var plus = new KleenePlus(node);
                        sequence.Add(plus);
                        current++;
                    }
                    break;
                case '*':
                    {
                        Console.WriteLine("Found a kleene star: *");
                        var node = sequence.PopLast();
                        var plus = new KleeneStar(node);
                        sequence.Add(plus);
                        current++;
                    }
                    break;
                case '?':
                    {
                        Console.WriteLine("Found an optional: ?");
                        var node = sequence.PopLast();
                        var plus = new Optional(node);
                        sequence.Add(plus);
                        current++;
                    }
                    break;
                case '.':
                    Console.WriteLine("Found a match all: .");
                    sequence.Add(new CharacterSet("."));
                    current++;
                    break;
                default:
                    Console.WriteLine($"Found a single character: {c}");
                    sequence.Add(new CharacterSet(c.ToString()));
                    current++;
                    break;
            }
        }
        return 0;
    }

    private Sequence ParseGroup(string pattern, ref int end)
    {
        Console.WriteLine($"Starting a group: {pattern}");
        var sequence = new Sequence();
        end = ParseSequence(pattern[1..], sequence);
        var group = pattern[1..end];
        Console.WriteLine($"Found a group: {group}");
        return sequence;
    }

    private CharacterSet ParseCharacterSet(string pattern)
    {
        Console.WriteLine($"Found a character set: [{pattern}]");
        return new CharacterSet(pattern);
    }
}