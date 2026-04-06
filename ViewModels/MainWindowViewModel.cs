using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.Models;
using GraphOptimizer.ViewModels.Helpers;
using GraphOptimizer.Interfaces;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IAppState
    {

        public GraphViewModel SharedGraphVM { get; } = new GraphViewModel(new Graph());

        public EditorContext EditorContext { get; } = new EditorContext();

        private bool _isAnalysisActive = false;
        public bool IsAnalysisActive
        {
            get => _isAnalysisActive;
            set => SetProperty(ref _isAnalysisActive, value);
        }

        public GraphEditorViewModel EditorVM { get; }
        public GraphTableViewModel TableVM { get; }
        public HeaderViewModel HeaderVM { get; }
        public AlgorithmAnalysisViewModel AnalysisVM { get; }

        public MainWindowViewModel()
        {
            EditorVM = new GraphEditorViewModel(SharedGraphVM, this, EditorContext);
            TableVM = new GraphTableViewModel(SharedGraphVM, this, EditorContext);
            HeaderVM = new HeaderViewModel(SharedGraphVM, this);
            AnalysisVM = new AlgorithmAnalysisViewModel(SharedGraphVM, this);

            HeaderVM.StartAnalysisRequested += () =>
            {
                IsAnalysisActive = true;
                AnalysisVM.Start();
            };

            HeaderVM.StopAnalysisRequested += () =>
            {
                AnalysisVM.Stop();
                IsAnalysisActive = false;
            };
        }
    }
}
