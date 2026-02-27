using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Views;

public partial class GraphEditorView : UserControl
{
    public GraphEditorViewModel? ViewModel => DataContext as GraphEditorViewModel;

    public GraphEditorView()
    {
        InitializeComponent();
    }

    public void OnPointerMovedInCanvas(object sender, PointerEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Control canvas)
        {
            var position = e.GetPosition((Visual)sender);
            ViewModel.HandlePointerMoved(position, canvas.Bounds.Size);
        }
    }

    public void OnPointerEnteredCanvas(object sender, PointerEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        
        ViewModel.HandlePointerEntered();
    }

    public void OnPointerExitedCanvas(object sender, PointerEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        //ViewModel.Cursor.IsInCanvas = false;
        ViewModel.HandlePointerExited();
    }

    public void OnPointerPressedCanvas(object sender, PointerPressedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        var pointerUpdateKind = e.GetCurrentPoint(this).Properties.PointerUpdateKind;

        if (pointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
        {
            var position = e.GetPosition((Visual)sender);

            ViewModel.HandleLeftClick(position);
        }
    }

    public void OnPointerReleasedCanvas(object sender, PointerReleasedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        var position = e.GetPosition((Visual)sender);

        ViewModel.HandlePointerReleased(position);
    }
}