namespace Core.LexicalAnalysis;

public class LexerOld
{
    private Dictionary<int, Dictionary<char, int>> transition_table = [];
    private Dictionary<int, Tuple<string,bool>> accept_states = [];
    private int start_state;

    public void Set(int start, Dictionary<int, Dictionary<char, int>> transition, Dictionary<int, Tuple<string, bool>> accept)
    {
        start_state = start;
        transition_table = transition;
        accept_states = accept;
    }

    public List<string> Run(string input, bool verbose_output = false)
    {
        List<string> result = [];

        while (input.Length > 0)
        {
            var token = NextToken(ref input, verbose_output);
            if (!string.IsNullOrEmpty(token))
                result.Add(token);
        }
        result.Add("[EndOfInput]");
        if (verbose_output)
            Console.WriteLine("[End of input]");
        return result;
    }

    public string NextToken(ref string input, bool verbose_output = false)
    {
        var states = new Stack<int>();
        var current_state = start_state;
        var next_state = -1;
        Tuple<string,bool> accept_state;
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
                    Console.WriteLine($"  Found accept state {accept_state.Item1} (skip={accept_state.Item2}), clearing stack");
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

        var output = input[..index];
        // Output found token + rule
        if (accept_state.Item2)
        {
            if (verbose_output)
                Console.WriteLine($"Skipping token {output} - rule {accept_state.Item1}");
            output = string.Empty;
        }
        else
        {
            if (verbose_output)
                Console.WriteLine($"Found token {output} - rule {accept_state.Item1}");
        }
        input = input.Remove(0, index);

        return output;
    }
}
