using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace GraphOptimizer.Views
{
    public partial class MainWindow : Window
    {
        private int CatCounter = 0;

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

        public void OnCatButtonClick(object? sender, RoutedEventArgs e)
        {
            CatCounter++;

            if (CatCounter == 3)
            {
                CatImage.Classes.Add("active");
            } else if (CatCounter == 4)
            {
                CatCounter = 0;
                CatImage.Classes.Remove("active");
            }
        }
    }
}