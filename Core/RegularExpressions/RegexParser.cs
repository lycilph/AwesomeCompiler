using System.Diagnostics;

namespace Core.RegularExpressions;

public class RegexParser
{
    private readonly string _pattern;
    private int current = 0;

    public RegexParser(string pattern)
    {
        _pattern = pattern;
    }

    public RegexNode Parse()
    {
        current = 0;
        return ParseSequence();
    }

    private SequenceNode ParseSequence()
    {
        var sequence = new SequenceNode();
        while (current < _pattern.Length)
        {
            var node = ParseNode();
            if (node == null)
                return sequence;

            switch (node)
            {
                case AlternationNode alternation:
                    alternation.Left = sequence.Last();
                    sequence.ReplaceLast(alternation);
                    break;
                case StarNode star:
                    star.Child = sequence.Last();
                    sequence.ReplaceLast(star);
                    break;
                case PlusNode plus:
                    plus.Child = sequence.Last();
                    sequence.ReplaceLast(plus);
                    break;
                case OptionalNode optional:
                    optional.Child = sequence.Last();
                    sequence.ReplaceLast(optional);
                    break;
                default:
                    sequence.Add(node);
                    break;
            }
        }
        return sequence;
    }

    private RegexNode? ParseNode()
    {
        RegexNode? node = null;

        var c = _pattern[current];
        switch (c)
        {
            case '[':
                var end = _pattern.IndexOf(']', current);
                if (end == -1)
                    Debug.WriteLine("Unmatched ] found");

                node = ParseCharacterSet(_pattern.Substring(current + 1, end - current - 1));
                current = end + 1;
                break;
            case '(':
                current++;
                node = new GroupNode(ParseSequence());
                break;
            case ')':
                current++;
                break;
            case '|':
                current++;
                var next_node = ParseNode();
                node = new AlternationNode() { Right = next_node };
                break;
            case '*':
                node = new StarNode();
                current++;
                break;
            case '+':
                node = new PlusNode();
                current++;
                break;
            case '?':
                node = new OptionalNode();
                current++;
                break;
            case '.':
                node = new MatchAnyCharacterNode();
                current++;
                break;
            case '\\':
                node = new MatchSingleCharacterNode(Peek());
                current += 2;
                break;
            default:
                node = new MatchSingleCharacterNode(c);
                current++;
                break;
        }

        return node;
    }

    private CharacterSetNode ParseCharacterSet(string setPattern)
    {
        var node = new CharacterSetNode();
        var setCurrent = 0;

        if (setPattern[setCurrent] == '^')
        {
            node.IsNegativeSet = true;
            setCurrent++;
        }

        char c = ' ';
        char c2 = ' ';
        while (setCurrent < setPattern.Length) 
        {
            c = setPattern[setCurrent];

            // Are we parsing a range
            if (setCurrent < setPattern.Length-1 && setPattern[setCurrent+1] == '-')
            { 
                c2 = setPattern[setCurrent+2];
                node.AddRange(c, c2);
                setCurrent += 3;
            }
            // Or is this just a single character
            else 
            {
                node.AddCharacter(c);
                setCurrent++;
            }
        }

        return node;
    }

    private char Peek() => _pattern[current + 1];
}
