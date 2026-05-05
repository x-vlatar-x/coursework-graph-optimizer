using Avalonia;
using GraphOptimizer.Enums;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.Services;
using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class HeaderViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public IAppState AppState { get; init; }

        public IFileService FileService { get; init; }

        private bool _isAnalysisModeListExpanded = false;
        public bool IsAnalysisModeListExpanded
        {
            get => _isAnalysisModeListExpanded;
            set => SetProperty(ref _isAnalysisModeListExpanded, value);
        }

        private AnalysisMode? _selectedAnalysisMode = null; 
        public AnalysisMode? SelectedAnalysisMode
        {
            get => _selectedAnalysisMode;
            set => SetProperty(ref _selectedAnalysisMode, value);
        }

        public event Action<AnalysisMode>? StartAnalysisRequested;
        public event Action StopAnalysisRequested;
        public event Action<AnalysisResult> AnalysisRestored;

        public HeaderViewModel(GraphViewModel graphVM, IAppState appState, IFileService fileService)
        {
            GraphVM = graphVM;
            AppState = appState;
            FileService = fileService;
        }

        public async Task HandleSaveProjectButtonClick(Visual visualRoot)
        {
            await FileService.SaveProjectAsync(visualRoot, GraphVM);
        }

        public async Task HandleLoadProjectButtonClick(Visual visualRoot)
        {
            var dto = await FileService.LoadProjectAsync(visualRoot);
            if (dto == null)
            {
                return;
            }

            GraphVM.Clear();

            foreach (var vertexDto in dto.Vertices)
            {
                GraphVM.AddNewVertexWithId(vertexDto.Id, vertexDto.X, vertexDto.Y);
            }

            foreach (var edgeDto in dto.Edges)
            {
                GraphVM.AddNewEdge(edgeDto.VertexId1, edgeDto.VertexId2);
            }

            if (dto is ResultDto resultDto)
            {
                AnalysisRestored.Invoke(resultDto.Result);
            }
        }

        public async void HandleClearButtonClick()
        {
            GraphVM.Clear();
        }

        public void HandleAnalysisModeListExpandButtonClick()
        {
            IsAnalysisModeListExpanded = !IsAnalysisModeListExpanded;
        }

        public void HandleActionButtonClick()
        {
            if (GraphVM.VerticesCount == 0 || SelectedAnalysisMode == null)
            {
                return;
            }

            if (AppState.IsAnalysisActive)
            {
                StopAnalysisRequested?.Invoke();
            }
            else
            {
                StartAnalysisRequested?.Invoke(SelectedAnalysisMode.Value);
            }
        }

        public void HandleAnalysisModeItemClick(AnalysisMode mode)
        {
            IsAnalysisModeListExpanded = false;
            SelectedAnalysisMode = mode;
        }
    }
}
