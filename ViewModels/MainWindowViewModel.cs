using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.Models;
using GraphOptimizer.ViewModels.Helpers;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        public GraphViewModel SharedGraphVM { get; } = new GraphViewModel(new Graph());

        public EditorContext EditorContext { get; } = new EditorContext();

        public GraphEditorViewModel Editor { get; }
        public GraphTableViewModel Table { get; }

        public MainWindowViewModel()
        {
            Editor = new GraphEditorViewModel(SharedGraphVM, EditorContext);
            Table = new GraphTableViewModel(SharedGraphVM, EditorContext);
        }
    }
}
