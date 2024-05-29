using Microsoft.Msagl.Drawing;
using System.Windows;

namespace AwesomCompilerGrammarIDE;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindowLoaded;
    }

    private void MainWindowLoaded(object sender, RoutedEventArgs e)
    {
        var graph = new Graph();

        var node = new Node("Start");
        node.Attr.AddStyle(Microsoft.Msagl.Drawing.Style.Dashed);
        graph.AddNode(node);

        var edge = graph.AddEdge("Octagon", "Hexagon");
        edge.LabelText = "[abc]";

        graph.AddEdge("Start", "Hexagon");

        graph.FindNode("Octagon").Attr.Shape = Shape.Octagon;
        graph.FindNode("Hexagon").Attr.Shape = Shape.Hexagon;

        graph.Attr.LayerDirection = LayerDirection.LR;

        graph_control.Graph = graph;
    }
}