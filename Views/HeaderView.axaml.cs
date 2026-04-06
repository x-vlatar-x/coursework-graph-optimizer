using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Views;

public partial class HeaderView : UserControl
{
    public HeaderViewModel? ViewModel => DataContext as HeaderViewModel;

    public HeaderView()
    {
        InitializeComponent();
    }

    public void OnAlgorithmListExpandButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        
        if (sender is Button)
        {
            ViewModel.HandleAlgorithmListExpandButtonClick();
            FlyoutBase.ShowAttachedFlyout(AlgorithmList);
        }
    }
}