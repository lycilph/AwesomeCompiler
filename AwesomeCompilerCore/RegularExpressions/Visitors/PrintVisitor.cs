using AwesomeCompilerCore.RegularExpressions.Nodes;
using System.Text;

namespace AwesomeCompilerCore.RegularExpressions.Visitors;

public class PrintVisitor : IVisitor
{
    private readonly StringBuilder sb = new StringBuilder();
    private int indent = 0;

    public void Visit(Regex node)
    {
        sb.AppendLine($"Root: {node.Pattern}");
        indent += 2;
        node.Root.Accept(this);
        indent -= 2;
    }

    public void Visit(AnyCharacterRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine("Any Character");
    }

    public void Visit(CharacterRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Character {node}");
    }

    public void Visit(CharacterSetRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Character set {node}");
    }

    public void Visit(AlternationRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Alternation |");
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }

    public void Visit(ConcatenationRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Concatenation .");
        indent += 2;
        node.Left.Accept(this);
        node.Right.Accept(this);
        indent -= 2;
    }
    
    public void Visit(StarRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Star *");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(PlusRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Plus +");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public void Visit(OptionalRegexNode node)
    {
        sb.Append(' ', indent);
        sb.AppendLine($"Optional ?");
        indent += 2;
        node.Child.Accept(this);
        indent -= 2;
    }

    public static string Run(RegexNode node) 
    { 
        var visitor = new PrintVisitor();
        node.Accept(visitor);
        return visitor.sb.ToString();
    }
}
