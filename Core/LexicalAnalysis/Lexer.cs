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

    public void Run(string input)
    {
        while (input.Length > 0)
        {
            NextToken(ref input);
        }
    }

    public void NextToken(ref string input)
    {
        var states = new Stack<int>();
        var current_state = start_state;
        var next_state = -1;
        var accept_state = string.Empty;
        var index = 0;
        char c;

        // Read ahead until stuck
        while (index < input.Length)
        {
            c = input[index];
            //Console.WriteLine($"Loop: current_state={current_state}, c={c}, # on stack {states.Count}");

            // Find transition from current_state on symbol c
            if (transition_table.TryGetValue(current_state, out var pair) && pair.TryGetValue(c, out next_state))
            {
                //Console.WriteLine($"  Found transition from {current_state} on {c} to {next_state}");
            }
            else
            {
                //Console.WriteLine($"  No transition found from {current_state} on {c}");
                break;
            }

            // Check for final/accept states
            if (accept_states.TryGetValue(current_state, out accept_state))
            {
                //Console.WriteLine($"  Found new final state {accept_state}");
                states.Clear();
            }
            else
                accept_state = string.Empty;

            states.Push(current_state);
            current_state = next_state;

            index++;
        }

        // Backtrack to last final state
        // TODO

        // Output found token + rule
        if (!accept_states.TryGetValue(current_state, out accept_state))
            throw new InvalidDataException($"Error - no accept state found for token (c={input[index]})");
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
