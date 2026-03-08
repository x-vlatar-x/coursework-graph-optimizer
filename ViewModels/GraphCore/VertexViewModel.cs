using GraphOptimizer.Models;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class VertexViewModel(Vertex model, double x, double y): ViewModelBase, IGraphObject
    {
        public Vertex Model { get; init; } = model;

        private double _x = x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y = y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        private bool _isHovered = false;
        public bool IsHovered
        {
            get => _isHovered;
            set => SetProperty(ref _isHovered, value);
        }

        private bool _isSelected = false;
        public bool IsSelected {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}