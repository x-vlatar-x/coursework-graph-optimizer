using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;
using GraphOptimizer.ViewModels;
using GraphOptimizer.ViewModels.GraphCore;
using Metsys.Bson;
using System.Diagnostics;

namespace GraphOptimizer.Views;

public partial class GraphTableView : UserControl
{
    public GraphTableViewModel? ViewModel => DataContext as GraphTableViewModel;

    public GraphTableView()
    {
        InitializeComponent();
    }

    public void OnExpandButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Button button && button.DataContext is VertexViewModel vertexVM)
        {
            ViewModel.HandleExpandButtonClick(vertexVM);
        }
    }

    public void OnNeighborDeletePressed(object? sender, PointerPressedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Border neighborBorder && neighborBorder.DataContext is VertexViewModel neighborVM)
        {
            var parentEntry = neighborBorder?.FindAncestorOfType<Border>(includeSelf: false);

            while (parentEntry != null && !(parentEntry.DataContext is VertexViewModel && parentEntry.Classes.Contains("VertexEntry")))
            {
                parentEntry = parentEntry?.FindAncestorOfType<Border>(includeSelf: false);
            }

            if (parentEntry != null && parentEntry.DataContext is VertexViewModel vertexVM)
            {
                ViewModel.HandleNeighborDeletePressed(vertexVM, neighborVM);
            }
        }
    }

    public void OnVertexAddPressed(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        if (sender is Button addButton)
        {
            ViewModel.HandleVertexAddPressed();
        }
    }

    public void OnVertexDeletePressed(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Button deleteButton && deleteButton.DataContext is VertexViewModel vertexVM)
        {
            ViewModel.HandleVertexDeletePressed(vertexVM);
        }
    }
}