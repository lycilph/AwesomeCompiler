using Core.RegularExpressions;
using Core.RegularExpressions.Algorithms;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AwesomeCompilerIDE;

public partial class MainWindow : Window
{
    private string pattern = string.Empty;
    private Node? root = null;

    public MainWindow()
    {
        InitializeComponent();
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

    private void ConvertToTreeview()
    {
        if (root != null)
        {
            treeview.Items.Clear();
            ConvertToTreeview(root, null);
        }
    }

    private void ConvertToTreeview(Node node, TreeViewItem? current)
    {
        TreeViewItem item = new TreeViewItem();
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
                item = new TreeViewItem() { Header = $"Character set [{(set.IsNegativeSet?"^ ":"")}{set.Count()} characters]", IsExpanded = true };
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
                Debug.WriteLine($"Unknown node type {node.GetType()}");
                break;
        }
    }

    private void AddItem(TreeViewItem? current, TreeViewItem item)
    {
        if (current == null)
            treeview.Items.Add(item);
        else
            current.Items.Add(item);
    }
}