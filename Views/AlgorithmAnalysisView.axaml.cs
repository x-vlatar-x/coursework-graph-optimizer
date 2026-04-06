using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Views;

public partial class AlgorithmAnalysisView : UserControl
{
    public AlgorithmAnalysisViewModel? ViewModel => DataContext as AlgorithmAnalysisViewModel;

    public AlgorithmAnalysisView()
    {
        InitializeComponent();
    }

    public void OnDragBarPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Border border && border.Name == "DragBar")
        {
            Point position = e.GetPosition(OverlayCanvas);
            ViewModel.HandleDragBarPointerPressed(position);
        }
    }

    public void OnDragBarPointerMoved(object sender, PointerEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Border border && border.Name == "DragBar")
        {
            e.Pointer.Capture(sender as Control);

            Point position = e.GetPosition(OverlayCanvas);
            Size canvasSize = OverlayCanvas.Bounds.Size;
            Size windowSize = Window.Bounds.Size;
            ViewModel.HandleDragBarPointerMoved(position, canvasSize, windowSize);
        }
    }

    public void OnDragBarPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Border border && border.Name == "DragBar")
        {
            ViewModel.HandleDragBarPointerReleased();
        }
    }
}