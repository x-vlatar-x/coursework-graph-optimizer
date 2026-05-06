using Avalonia;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class GraphViewModel : ViewModelBase, IAdjacencyContext
    {
        public Graph Model { get; init; }
        //public ObservableCollection<IGraphObject> GraphObjects { get; } = [];
        public ObservableCollection<VertexViewModel> Vertices { get; } = [];
        public ObservableCollection<EdgeViewModel> Edges { get; } = [];

        public int VerticesCount => Vertices.Count;
        public int EdgesCount => Edges.Count;

        public GraphViewModel(Graph model)
        {
            Model = model;

            Vertices.CollectionChanged += (s, e) =>
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i].DisplayIndex = i + 1;
                }
            };
        }

        public VertexViewModel? AddNewVertex(double x, double y)
        {
            if (Vertices.Count >= AppConstants.MaxVertiecesCount)
            {
                return null;
            }

            var vertexModel = Model.AddNewVertex();

            var vertexViewModel = new VertexViewModel(this, vertexModel, x, y);

            //GraphObjects.Add(vertexViewModel);
            Vertices.Add(vertexViewModel);

            OnPropertyChanged(nameof(VerticesCount));

            return vertexViewModel;
        }

        public VertexViewModel? AddNewVertex(Point position)
        {
            return AddNewVertex(position.X, position.Y);
        }

        public VertexViewModel? AddNewVertexWithId(uint id, double x, double y)
        {
            if (Vertices.Count >= AppConstants.MaxVertiecesCount)
            {
                return null;
            }

            var vertexModel = Model.AddNewVertexWithId(id);

            var vertexViewModel = new VertexViewModel(this, vertexModel, x, y);

            Vertices.Add(vertexViewModel);

            OnPropertyChanged(nameof(VerticesCount));

            return vertexViewModel;
        }

        public VertexViewModel? AddNewVertexWithId(uint id, Point position)
        {
            return AddNewVertexWithId(id, position.X, position.Y);
        }

        public EdgeViewModel? AddNewEdge(VertexViewModel vertexVM1, VertexViewModel vertexVM2)
        {
            if (EdgeExists(vertexVM1, vertexVM2))
            {
                return null;
            }

            var edgeModel = Model.AddNewEdge(vertexVM1.Model, vertexVM2.Model);

            var edgeViewModel = new EdgeViewModel(edgeModel, vertexVM1, vertexVM2);

            Edges.Add(edgeViewModel);

            vertexVM1.NotifyEdgeCountChanged();
            vertexVM2.NotifyEdgeCountChanged();
            vertexVM1.NotifyNeighborsChanged();
            vertexVM2.NotifyNeighborsChanged();
            OnPropertyChanged(nameof(EdgesCount));

            return edgeViewModel;
        }

        public EdgeViewModel? AddNewEdge(uint vertexId1, uint vertexId2)
        {
            var vertexVM1 = Vertices.First(vertexVM => vertexVM.Model.Id == vertexId1);
            var vertexVM2 = Vertices.First(vertexVM => vertexVM.Model.Id == vertexId2);

            return AddNewEdge(vertexVM1, vertexVM2);
        }

        public void RemoveVertex(VertexViewModel vertexVM)
        {
            Vertices.Remove(vertexVM);
            Model.RemoveVertex(vertexVM.Model);

            var edgesToRemove = Edges
                .Where(edge => edge.VertexVM1 == vertexVM || edge.VertexVM2 == vertexVM)
                .ToList();

            OnPropertyChanged(nameof(VerticesCount));
            OnPropertyChanged(nameof(EdgesCount));

            foreach (var edgeVM in edgesToRemove)
            {
                RemoveEdge(edgeVM);
            }
        }

        public void RemoveEdge(EdgeViewModel edgeVM)
        {
            Edges.Remove(edgeVM);

            Model.RemoveEdge(edgeVM.Model);

            edgeVM.VertexVM1.NotifyEdgeCountChanged();
            edgeVM.VertexVM2.NotifyEdgeCountChanged();
            edgeVM.VertexVM1.NotifyNeighborsChanged();
            edgeVM.VertexVM2.NotifyNeighborsChanged();
            OnPropertyChanged(nameof(EdgesCount));
        }

        public void RemoveEdge(VertexViewModel vertexVM1, VertexViewModel vertexVM2)
        {
            var edgeVM = Edges.First(edge =>
                (edge.VertexVM1 == vertexVM1 && edge.VertexVM2 == vertexVM2)
                || (edge.VertexVM1 == vertexVM2 && edge.VertexVM2 == vertexVM1));

            if (edgeVM == null)
            {
                return;
            }

            RemoveEdge(edgeVM);
            OnPropertyChanged(nameof(EdgesCount));
        }

        public void Clear()
        {
            Edges.Clear();
            Vertices.Clear();
            Model.Clear();
            OnPropertyChanged(nameof(VerticesCount));
            OnPropertyChanged(nameof(EdgesCount));
        }

        public void ApplyVertexCover(List<uint> vertexIds)
        {
            var idSet = new HashSet<uint>(vertexIds);

            foreach (var vertex in Vertices)
            {
                vertex.IsInVertexCover = idSet.Contains(vertex.Model.Id);
                //vertex.Model.IsInVertexCover = vertex.IsInVertexCover;
            }
        }

        public void ClearVertexCover()
        {
            foreach (var vertex in Vertices)
            {
                vertex.IsInVertexCover = false;
                //vertex.Model.IsInVertexCover = false;
            }
        }

        public bool EdgeExists(VertexViewModel vertexVM1, VertexViewModel vertexVM2)
        {
            if (vertexVM1 == null || vertexVM2 == null)
            {
                return false;
            }

            return Edges.Any(edgeVM =>
                (edgeVM.VertexVM1 == vertexVM1 && edgeVM.VertexVM2 == vertexVM2) ||
                (edgeVM.VertexVM1 == vertexVM2 && edgeVM.VertexVM2 == vertexVM1)
            );
        }

        public bool EdgeExists(uint vertexId1, uint vertexId2)
        {
            return Edges.Any(edgeVM =>
                (edgeVM.VertexVM1.Model.Id == vertexId1 && edgeVM.VertexVM2.Model.Id == vertexId2) ||
                (edgeVM.VertexVM1.Model.Id == vertexId2 && edgeVM.VertexVM2.Model.Id == vertexId1)
            );
        }

        public bool VertexExists(VertexViewModel vertexVM)
        {
            if (vertexVM == null)
            {
                return false;
            }
            return Vertices.Any(v => v == vertexVM);
        }

        public bool VertexExists(uint vertexId)
        {
            return Vertices.Any(v => v.Model.Id == vertexId);
        }

        public IEnumerable<EdgeViewModel> GetEdgesForVertex(VertexViewModel vertexVM)
        {
            return Edges.Where(edgeVM => edgeVM.VertexVM1 == vertexVM || edgeVM.VertexVM2 == vertexVM);
        }

        public IEnumerable<VertexViewModel> GetNeighborsForVertex(VertexViewModel vertexVM)
        {
            return GetEdgesForVertex(vertexVM).Select(edgeVM => edgeVM.VertexVM1 == vertexVM ? edgeVM.VertexVM2 : edgeVM.VertexVM1);
        }
    }
}