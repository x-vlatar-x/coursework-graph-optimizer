using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GraphOptimizer.Enums;
using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Views;

public partial class HeaderView : UserControl
{
    public HeaderViewModel? ViewModel => DataContext as HeaderViewModel;

    public HeaderView()
    {
        InitializeComponent();
    }

    public void OnSaveProjectButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Button button && button.Name == "SaveButton")
        {
            ViewModel.HandleSaveProjectButtonClick(this);
        }
    }

    public void OnLoadProjectButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }

        if (sender is Button button && button.Name == "LoadButton")
        {
            ViewModel.HandleLoadProjectButtonClick(this);
        }
    }

    public void OnAnalysisModeListExpandButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        
        if (ViewModel.AppState.IsAnalysisActive)
        {
            return;
        }

        if (sender is Button)
        {
            ViewModel.HandleAnalysisModeListExpandButtonClick();
            FlyoutBase.ShowAttachedFlyout(AnalysisModeList);
        }
    }

    public void OnActionButtonClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        if (sender is Button button && button.Name == "ActionButton")
        {
            ViewModel.HandleActionButtonClick();
        }
    }

    public void OnAnalysisModeItemClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel == null)
        {
            return;
        }
        if (sender is Button button && button.Tag is AnalysisMode mode)
        {
            var flyout = FlyoutBase.GetAttachedFlyout(AnalysisModeList);

            if (flyout == null)
            {
                return;
            }

            flyout.Hide();
            ViewModel.HandleAnalysisModeItemClick(mode);
        }
    }
}