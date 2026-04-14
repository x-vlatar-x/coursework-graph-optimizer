using Avalonia;
using GraphOptimizer.Enums;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.Services;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class AlgorithmAnalysisViewModel : ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public IAppState AppState { get; init; }

        public EditorContext EditorContext { get; init; }

        public IVertexCoverService VertexCoverService { get; init; }
        public IFileService FileService { get; init; }

        private AnalysisResult _result = new AnalysisResult(null, [], 0, 0, 0);
        public AnalysisResult Result { 
            get => _result;
            private set => SetProperty(ref _result, value); 
        }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        private double _x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        private bool _isDragging = false;
        private Point _dragOffset;

        public AlgorithmAnalysisViewModel(GraphViewModel graphVM, IAppState appState, EditorContext editorContext, IVertexCoverService vertexCoverService, IFileService fileService)
        {
            GraphVM = graphVM;
            AppState = appState;
            EditorContext = editorContext;
            VertexCoverService = vertexCoverService;
            FileService = fileService;
        }

        public void Start(AnalysisMode analysisMode)
        {
            EditorContext.StopHovering();
            EditorContext.StopDragging();
            EditorContext.StopSelecting();
            EditorContext.StopConnecting();
            EditorContext.ClearSelection();
            EditorContext.SelectedTool = EditorTool.Move;

            //List<uint> vertexCoverIds = VertexCoverService.Solve(GraphVM.Model, analysisMode);
            Result = VertexCoverService.Solve(GraphVM.Model, analysisMode);
            GraphVM.ApplyVertexCover(Result.VertexCoverIds);

            IsExpanded = true;
        }

        public void Stop()
        {
            IsExpanded = false;
            GraphVM.ClearVertexCover();
            //IsVisible = false;
        }

        public void Load(AnalysisResult analysisResult)
        {
            EditorContext.StopHovering();
            EditorContext.StopDragging();
            EditorContext.StopSelecting();
            EditorContext.StopConnecting();
            EditorContext.ClearSelection();
            EditorContext.SelectedTool = EditorTool.Move;

            Result = analysisResult;
            GraphVM.ApplyVertexCover(Result.VertexCoverIds);

            IsExpanded = true;
        }

        public void HandleDragBarPointerPressed(Point position)
        {
            _isDragging = true;
            _dragOffset = new Point(position.X - X, position.Y - Y);
        }

        public void HandleDragBarPointerMoved(Point mousePosition, Size canvasSize, Size windowSize)
        {
            if (!_isDragging)
            {
                return;
            }

            double newX = mousePosition.X - _dragOffset.X;
            double newY = mousePosition.Y - _dragOffset.Y;

            X = Math.Clamp(newX, 0, canvasSize.Width - windowSize.Width);
            Y = Math.Clamp(newY, 0, canvasSize.Height - windowSize.Height);
        }

        public void HandleDragBarPointerReleased()
        {
            _isDragging = false;
            _dragOffset = new Point(0, 0);
        }

        public void HandleExpandButtonClick()
        {
            IsExpanded = !IsExpanded;
        }

        public async Task HandleSaveResultButtonClick(Visual visualRoot)
        {
            await FileService.SaveResultAsync(visualRoot, GraphVM, Result);
        }
    }
}
