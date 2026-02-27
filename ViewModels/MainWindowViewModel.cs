using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.Models;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public GraphViewModel SharedGraph { get; } = new GraphViewModel(new Graph());

        public GraphEditorViewModel Editor { get; }
        public GraphMatrixViewModel Matrix { get; }

        public MainWindowViewModel()
        {
            Editor = new GraphEditorViewModel(SharedGraph);
            Matrix = new GraphMatrixViewModel(SharedGraph);
        }
    }
}
