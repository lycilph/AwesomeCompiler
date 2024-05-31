using AwesomeCompilerCore.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AwesomeCompilerIDE;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RegexTextKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            ParseRegex();
    }

    private void ParseRegex()
    {
        var tokens = RegexTokenizer.Tokenize(regex_textbox.Text);
        foreach (var token in tokens)
            tokens_listbox.Items.Add(token);
    }
}