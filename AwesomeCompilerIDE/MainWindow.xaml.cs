using Core.NFA;
using Core.NFA.Algorithms;
using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace AwesomeCompilerIDE;

public partial class MainWindow : Window
{
    private const string dotInput = "graph.txt";
    private const string dotOutput = "graph.png";

    private string pattern = string.Empty;
    private RegexNode? root;
    private Graph? nfa;

    public MainWindow()
    {
        InitializeComponent();

        image.ImageFailed += Image_ImageFailed;
    }

    private void Image_ImageFailed(object? sender, ExceptionRoutedEventArgs e)
    {
        Debug.WriteLine("Failed to load image");
    }

    private void ParseButtonClick(object sender, RoutedEventArgs e)
    {
        var range = new TextRange(textbox.Document.ContentStart, textbox.Document.ContentEnd);
        pattern = range.Text.Trim();
        
        if (!string.IsNullOrWhiteSpace(pattern))
        {
            var regex = new Regex(pattern);
            root = regex.GetRoot();

            ConvertToTreeview();
        }
    }

    private void SimplifyButtonClick(object sender, RoutedEventArgs e)
    {
        if (root != null)
        {
            var visitor = new SimplifyVisitor();
            root.Accept(visitor);

            ConvertToTreeview();
        }
    }

    private void ConvertToNFAButtonClick(object sender, RoutedEventArgs e)
    {
        if (root != null)
        {
            try
            {
                nfa = root.ConvertToNFA();
                var dotGraph = DotGraphGenerator.Generate(nfa.Start);
                File.WriteAllText(dotInput, dotGraph);

                GenerateDotGraph();
            }
            catch (NotImplementedException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }


    private void ConvertToDFAButtonClick(object sender, RoutedEventArgs e)
    {
        if (nfa != null)
        {
            var sc = new SubsetConstruction();
            var dfa = sc.Execute(nfa);
            var dotGraph = DotGraphGenerator.Generate(dfa);
            File.WriteAllText(dotInput, dotGraph);

            GenerateDotGraph();
        }
    }

    private void GenerateDotGraph()
    {
        var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "dot.exe",
            Arguments = $"{dotInput} -Tpng -o{dotOutput}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.StartInfo = startInfo;
        process.Start();

        Debug.WriteLine("std out: " + process.StandardOutput.ReadToEnd());
        Debug.WriteLine("std err: " + process.StandardError.ReadToEnd());

        process.WaitForExit();

        // Image must be loaded through a stream, so that it does not lock the file
        var path = Path.GetFullPath(dotOutput);
        var bitmap = new BitmapImage();
        var stream = File.OpenRead(path);

        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        stream.Close();
        stream.Dispose();

        image.Source = bitmap;
    }

    private void ConvertToTreeview()
    {
        if (root != null)
        {
            treeview.Items.Clear();
            ConvertToTreeview(root, null);
        }
    }

    private void ConvertToTreeview(RegexNode node, TreeViewItem? current)
    {
        TreeViewItem item;
        switch (node)
        {
            case SequenceNode sequence:
                item = new TreeViewItem() { Header = "Sequence", IsExpanded = true };
                AddItem(current, item);
                foreach (var n in sequence.Children)
                    ConvertToTreeview(n, item);
                break;
            case AlternationNode alternation:
                item = new TreeViewItem() { Header = "Alternation (|)", IsExpanded = true };
                AddItem(current, item);
                ConvertToTreeview(alternation.Left!, item);
                ConvertToTreeview(alternation.Right!, item);
                break;
            case GroupNode group:
                item = new TreeViewItem() { Header = "Group ()", IsExpanded = true };
                AddItem(current, item);
                ConvertToTreeview(group.Child!, item);
                break;
            case OptionalNode optional:
                item = new TreeViewItem() { Header = "Optional ? (0 or 1)", IsExpanded = true };
                AddItem(current, item);
                ConvertToTreeview(optional.Child!, item);
                break;
            case PlusNode plus:
                item = new TreeViewItem() { Header = "Plus + (1 or more)", IsExpanded = true };
                AddItem(current, item);
                ConvertToTreeview(plus.Child!, item);
                break;
            case StarNode star:
                item = new TreeViewItem() { Header = "Star * (0 or more)", IsExpanded = true };
                AddItem(current, item);
                ConvertToTreeview(star.Child!, item);
                break;
            case CharacterSetNode set:
                item = new TreeViewItem() { Header = $"Character set [{set.Label}] {set.Count()} characters", IsExpanded = true };
                AddItem(current, item);
                break;
            case MatchSingleCharacterNode matchSingleCharacter:
                item = new TreeViewItem() { Header = $"Single Char [{matchSingleCharacter.Value}]", IsExpanded = true };
                AddItem(current, item);
                break;
            case MatchAnyCharacterNode matchAnyCharacter:
                item = new TreeViewItem() { Header = $"Any Char [.]", IsExpanded = true };
                AddItem(current, item);
                break;
            default:
                throw new ArgumentException($"Unknown node type {node.GetType()}");
        }
    }

    private void AddItem(TreeViewItem? current, TreeViewItem item)
    {
        if (current == null)
            treeview.Items.Add(item);
        else
            current.Items.Add(item);
    }

    private void Exp1ButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
        textbox.Document.Blocks.Add(new Paragraph(new Run("[0-9]+(\\.[0-9]+(a|b)(g*))?")));
    }

    private void Exp2ButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
        textbox.Document.Blocks.Add(new Paragraph(new Run("[0-9]")));
    }

    private void Exp3ButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
        textbox.Document.Blocks.Add(new Paragraph(new Run("(a|b)*abb")));
    }

    private void Exp4ButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
        textbox.Document.Blocks.Add(new Paragraph(new Run("[a-z]([a-z]|[0-9]|_)*")));
    }

    private void Exp5ButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
        textbox.Document.Blocks.Add(new Paragraph(new Run("[a-z]([^0-9])*")));
    }

    private void ClearButtonClick(object sender, RoutedEventArgs e)
    {
        textbox.Document.Blocks.Clear();
    }
}