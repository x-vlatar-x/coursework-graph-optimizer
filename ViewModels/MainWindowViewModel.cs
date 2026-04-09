using GraphOptimizer.Enums;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.Services;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IAppState
    {

        public GraphViewModel SharedGraphVM { get; } = new GraphViewModel(new Graph());
        public IVertexCoverService VertexCoverService { get; init; }

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
            VertexCoverService = new VertexCoverService();

            EditorVM = new GraphEditorViewModel(SharedGraphVM, this, EditorContext);
            TableVM = new GraphTableViewModel(SharedGraphVM, this, EditorContext);
            HeaderVM = new HeaderViewModel(SharedGraphVM, this);
            AnalysisVM = new AlgorithmAnalysisViewModel(SharedGraphVM, this, VertexCoverService);

            HeaderVM.StartAnalysisRequested += (AnalysisMode analysisMode) =>
            {
                IsAnalysisActive = true;
                AnalysisVM.Start(analysisMode);
            };

            HeaderVM.StopAnalysisRequested += () =>
            {
                AnalysisVM.Stop();
                IsAnalysisActive = false;
            };
        }
    }
}
