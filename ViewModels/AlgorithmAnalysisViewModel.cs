using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using GraphOptimizer.Interfaces;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class AlgorithmAnalysisViewModel : ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public IAppState AppState { get; init; }

        //public AnalysisContext AnalysisContext { get; init; } = new AnalysisContext();

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

        //private bool _isVisible = true;
        //public bool IsVisible
        //{
        //    get => _isVisible;
        //    set => SetProperty(ref _isVisible, value);
        //}

        public AlgorithmAnalysisViewModel(GraphViewModel graphVM, IAppState appState)
        {
            GraphVM = graphVM;
            AppState = appState;
        }

        public void Start()
        {
            //IsVisible = true;
        }

        public void Stop()
        {
            //IsVisible = false;
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
    }
}
