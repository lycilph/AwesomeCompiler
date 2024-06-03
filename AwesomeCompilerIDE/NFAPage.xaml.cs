using AwesomeCompilerCore.Graphs.NFAAlgorithms;
using AwesomeCompilerCore.RegularExpressions;
using AwesomeCompilerCore.RegularExpressions.Visitors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AwesomeCompilerIDE;

public partial class NFAPage : UserControl
{
    public NFAPage()
    {
        InitializeComponent();
    }

    private void RegexTextKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            ConverToNFA();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ConverToNFA();
    }

    private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (regex_textbox == null)
            return;

        if (((ComboBoxItem)examples_combobox.SelectedValue).Content is string str)
        {
            if (str == "Clear")
                regex_textbox.Text = "";
            else
                regex_textbox.Text = str;
        }
    }

    private void ConverToNFA()
    {
        tokens_listbox.Items.Clear();
        graph_Control.Graph = new Microsoft.Msagl.Drawing.Graph();
        if (string.IsNullOrWhiteSpace(regex_textbox.Text))
            return;

        var tokens = RegexTokenizer.Tokenize(regex_textbox.Text);
        foreach (var token in tokens)
            tokens_listbox.Items.Add(token);

        var regex = new Regex(regex_textbox.Text);
        var nfa = RegexToNFAVisitor.Run(regex);

        var graphWalker = new NFAGraphWalker();
        graphWalker.WalkGraph(nfa.Start);

        graph_Control.Graph = graphWalker.Graph;
    }
}
