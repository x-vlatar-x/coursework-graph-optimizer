using GraphOptimizer.Enums;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.Services;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System.Diagnostics;

namespace GraphOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IAppState
    {

        public GraphViewModel SharedGraphVM { get; } = new GraphViewModel(new Graph());
        public IVertexCoverService VertexCoverService { get; init; }
        public IFileService FileService { get; init; }

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
            FileService = new FileService();

            EditorVM = new GraphEditorViewModel(SharedGraphVM, this, EditorContext);
            TableVM = new GraphTableViewModel(SharedGraphVM, this, EditorContext);
            HeaderVM = new HeaderViewModel(SharedGraphVM, this, FileService);
            AnalysisVM = new AlgorithmAnalysisViewModel(SharedGraphVM, this, EditorContext, VertexCoverService, FileService);

            HeaderVM.StartAnalysisRequested += (AnalysisMode analysisMode) =>
            {
                AnalysisVM.Start(analysisMode);
                IsAnalysisActive = true;
            };

            HeaderVM.StopAnalysisRequested += () =>
            {
                IsAnalysisActive = false;
                AnalysisVM.Stop();
            };

            HeaderVM.AnalysisRestored += (AnalysisResult analysisResult) =>
            {
                AnalysisVM.Load(analysisResult);
                IsAnalysisActive = true;
            };
        }
    }
}
