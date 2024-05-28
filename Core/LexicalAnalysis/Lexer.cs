namespace Core.LexicalAnalysis;

public class Lexer
{
    private Dictionary<int, Dictionary<char, int>> transition_table = [];
    private Dictionary<int, string> accept_states = [];
    private int start_state;

    public void Set(int start, Dictionary<int, Dictionary<char, int>> transition, Dictionary<int, string> accept)
    {
        start_state = start;
        transition_table = transition;
        accept_states = accept;
    }

    public void Run(string input, bool verbose_output = false)
    {
        while (input.Length > 0)
        {
            NextToken(ref input, verbose_output);
        }
        Console.WriteLine("[End of input]");
    }

    public void NextToken(ref string input, bool verbose_output = false)
    {
        var states = new Stack<int>();
        var current_state = start_state;
        var next_state = -1;
        var accept_state = string.Empty;
        var index = 0;
        char c;

        if (verbose_output)
            Console.WriteLine($"Input:{input}");

        // Check if current_state is a final/accept state
        accept_states.TryGetValue(current_state, out accept_state);

        // Read ahead until stuck
        while (index < input.Length)
        {
            c = input[index];
            

            if (verbose_output)
                Console.WriteLine($"Loop: current_state={current_state}, accepts={accept_state}, c={c}, Stack={string.Join(",", states)}");

            // Find transition from current_state on symbol c
            if (transition_table.TryGetValue(current_state, out var pair) && pair.TryGetValue(c, out next_state))
            {
                if (verbose_output)
                    Console.WriteLine($"  Found transition from {current_state} on {c} to {next_state}");
            }
            else
            {
                if (verbose_output)
                    Console.WriteLine($"  No transition found from {current_state} on {c}");
                break;
            }

            // Handle final states
            if (accept_state != null)
            {
                states.Clear();
                if (verbose_output)
                    Console.WriteLine($"  Found accept state {accept_state}, clearing stack");
            }

            states.Push(current_state);
            current_state = next_state;
            accept_states.TryGetValue(current_state, out accept_state);  // Check if current_state is a final/accept state
            index++;
        }

        if (accept_state == null && verbose_output)
            Console.WriteLine("Backtracking");

        // Backtrack to last final state
        while (accept_state == null)
        {
            if (states.Count == 0)
                throw new InvalidDataException("Could not find a match for input");

            if (verbose_output)
                Console.WriteLine($"Loop: current_state={current_state}, accepts={accept_state}, Stack={string.Join(",", states)}");

            current_state = states.Pop();
            accept_states.TryGetValue(current_state, out accept_state);
            index--;
        }

        // Output found token + rule
        Console.WriteLine($"Found token {input[..index]} - rule {accept_state}");
        input = input.Remove(0, index);
    }

    //private void WriteTablesToFile()
    //{
    //    var str = JsonSerializer.Serialize(transition_table);
    //    File.WriteAllText("transition_table.txt", str);

    //    str = JsonSerializer.Serialize(accept_states);
    //    File.WriteAllText("accept_states.txt", str);
    //}
}
