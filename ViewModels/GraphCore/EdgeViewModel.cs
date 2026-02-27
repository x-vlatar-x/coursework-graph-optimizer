using Avalonia;
using GraphOptimizer.Models;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class EdgeViewModel: ViewModelBase, IGraphObject
    {
        public Edge Model { get; init; }

        public VertexViewModel VertexVM1 { get; }
        public VertexViewModel VertexVM2 { get; }

        public Point StartPoint => new Point(VertexVM1.X, VertexVM1.Y);
        public Point EndPoint => new Point(VertexVM2.X, VertexVM2.Y);
        public EdgeViewModel(Edge model, VertexViewModel vertex1, VertexViewModel vertex2)
        {
            Model = model;
            VertexVM1 = vertex1;
            VertexVM2 = vertex2;

            VertexVM1.PropertyChanged += (s, e) => {
                if (e.PropertyName is "X" or "Y") { 
                    OnPropertyChanged(nameof(StartPoint)); 
                } 
            };
            VertexVM2.PropertyChanged += (s, e) => { 
                if (e.PropertyName is "X" or "Y") {
                OnPropertyChanged(nameof(EndPoint));
                }
            };
        }

        private bool _isHovered = false;
        public bool IsHovered
        {
            get => _isHovered;
            set => SetProperty(ref _isHovered, value);
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
