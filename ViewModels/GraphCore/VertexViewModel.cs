using GraphOptimizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class VertexViewModel(IAdjacencyContext adjacencyContext, Vertex model, double x, double y) : ViewModelBase, IGraphObject
    {
        private readonly IAdjacencyContext _adjacencyContext = adjacencyContext;
        public Vertex Model { get; init; } = model;

        // Editor state
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

        // List state
        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public void ToggleExpansion()
        {
            IsExpanded = !IsExpanded;
            NotifyNeighborsChanged();
        }

        public int EdgeCount => _adjacencyContext.GetEdgesForVertex(this).Count();

        public IEnumerable<VertexViewModel> Neighbors => _adjacencyContext.GetNeighborsForVertex(this);

        public void NotifyEdgeCountChanged()
        {
            OnPropertyChanged(nameof(EdgeCount));
        }

        public void NotifyNeighborsChanged()
        {
            if (IsExpanded)
            {
                OnPropertyChanged(nameof(Neighbors));
            }
        }

        private string _inputNeighborId = "";
        public string InputNeighborId
        {
            get => _inputNeighborId;
            set
            {
                SetProperty(ref _inputNeighborId, value);
                OnPropertyChanged(nameof(IsIdEmpty));
                OnPropertyChanged(nameof(IsIdValid));
            }
        }

        public bool IsIdEmpty => string.IsNullOrEmpty(InputNeighborId);

        public bool IsIdValid => !string.IsNullOrEmpty(InputNeighborId)
                            && uint.TryParse(InputNeighborId, out uint id) 
                            && _adjacencyContext.VertexExists(id)
                            && !(_adjacencyContext.EdgeExists(Model.Id, id))
                            && id != Model.Id;
    }
}