using AwesomeCompilerCore.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AwesomeCompilerIDE;

public partial class RegexPage : UserControl
{
    public RegexPage()
    {
        InitializeComponent();
    }

    private void RegexTextKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            ParseRegex();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ParseRegex();
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

    private void ParseRegex()
    {
        tokens_listbox.Items.Clear();
        graph_Control.Graph = new Microsoft.Msagl.Drawing.Graph();
        if (string.IsNullOrWhiteSpace(regex_textbox.Text))
            return;
        
        var tokens = RegexTokenizer.Tokenize(regex_textbox.Text);
        foreach (var token in tokens)
            tokens_listbox.Items.Add(token);

        var regex = new Regex(regex_textbox.Text);
        var visitor = new RegexGraphVisitor();
        regex.Accept(visitor);

        graph_Control.Graph = visitor.Graph;
    }
}
