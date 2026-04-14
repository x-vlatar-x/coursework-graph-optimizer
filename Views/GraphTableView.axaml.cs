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
using System;
using System.Diagnostics;
using System.Linq;

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

    public void OnNeighborAddPressed(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Button addButton && addButton.DataContext is VertexViewModel vertexVM)
        {
            ViewModel.HandleNeighborAddPressed(vertexVM);
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

    public void OnIdTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (ViewModel.AppState.IsAnalysisActive)
        {
            return;
        }

        if (sender is TextBox textBox && textBox != null && textBox.DataContext is VertexViewModel vertexVM)
        {
            string cleanText = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textBox.Text != cleanText)
            {
                int caretIndex = textBox.CaretIndex;
                //textBox.Text = cleanText;
                vertexVM.InputNeighborId = cleanText;
                //textBox.CaretIndex = Math.Min(caretIndex, cleanText.Length);
            }
        }
    }
}