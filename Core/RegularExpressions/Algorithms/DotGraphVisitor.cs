using System.Text;

namespace Core.RegularExpressions.Algorithms;

public class DotGraphVisitor : IVisitor<int>
{
    private StringBuilder sb = new StringBuilder();

    public static string Generate(RegexNode node)
    {
        var visitor = new DotGraphVisitor();
        visitor.Begin();
        node.Accept(visitor);
        visitor.End();
        return visitor.sb.ToString();
    }

    public void Begin()
    {
        sb.AppendLine("digraph {");
    }

    public void End()
    {
        sb.AppendLine("}");
    }

    public int Visit(CharacterNode node)
    {
        sb.AppendLine($"  {node.Id} [shape=circle,label={node.Value}]");
        return node.Id;
    }

    public int Visit(AlternationNode node)
    {
        var leftId = node.Left.Accept(this);
        var rightId = node.Right.Accept(this);
        sb.AppendLine($"  {node.Id} [shape=circle,label=\"|\"]");
        sb.AppendLine($"  {node.Id} -> {leftId}");
        sb.AppendLine($"  {node.Id} -> {rightId}");
        return node.Id;
    }

    public int Visit(ConcatenationNode node)
    {
        var leftId = node.Left.Accept(this);
        var rightId = node.Right.Accept(this);
        sb.AppendLine($"  {node.Id} [shape=circle,label=\".\"]");
        sb.AppendLine($"  {node.Id} -> {leftId}");
        sb.AppendLine($"  {node.Id} -> {rightId}");
        return node.Id;
    }

    public int Visit(StarNode node)
    {
        var childId = node.Child.Accept(this);
        sb.AppendLine($"  {node.Id} [shape=circle,label=\"*\"]");
        sb.AppendLine($"  {node.Id} -> {childId}");
        return node.Id;
    }

    public int Visit(PlusNode node)
    {
        var childId = node.Child.Accept(this);
        sb.AppendLine($"  {node.Id} [shape=circle,label=\"+\"]");
        sb.AppendLine($"  {node.Id} -> {childId}");
        return node.Id;
    }

    public int Visit(OptionalNode node)
    {
        var childId = node.Child.Accept(this);
        sb.AppendLine($"  {node.Id} [shape=circle,label=\"?\"]");
        sb.AppendLine($"  {node.Id} -> {childId}");
        return node.Id;
    }
}
