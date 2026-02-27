using Avalonia;
using System;

namespace GraphOptimizer.ViewModels.Helpers
{
    public class MouseState: ViewModelBase
    {
        private Point _position;
        public Point Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private bool _isInCanvas;
        public bool IsInCanvas
        {
            get => _isInCanvas;
            set => SetProperty(ref _isInCanvas, value);
        }

        public void UpdatePosition(Point position)
        {
            Position = position;
        }
    }
}
