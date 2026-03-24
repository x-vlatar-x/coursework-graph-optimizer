using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.Models;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public GraphViewModel SharedGraphVM { get; } = new GraphViewModel(new Graph());

        public GraphEditorViewModel Editor { get; }
        public GraphTableViewModel Table { get; }

        public MainWindowViewModel()
        {
            Editor = new GraphEditorViewModel(SharedGraphVM);
            Table = new GraphTableViewModel(SharedGraphVM);
        }
    }
}
