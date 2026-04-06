using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace GraphOptimizer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.AddHandler(PointerPressedEvent, (s, e) =>
            {
                var visual = e.Source as Visual;
                bool isInsideTextBox = false;

                while (visual != null)
                {
                    if (visual is TextBox)
                    {
                        isInsideTextBox = true;
                        break;
                    }
                    visual = visual.GetVisualParent();
                }

                if (!isInsideTextBox)
                {
                    TopLevel.GetTopLevel(this)?.FocusManager?.ClearFocus();
                }
            }, RoutingStrategies.Tunnel);
        }
    
        public void OnAlgoritmsListExpandButtonClick(object? sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(AlgorithmsList);
        }
    }
}