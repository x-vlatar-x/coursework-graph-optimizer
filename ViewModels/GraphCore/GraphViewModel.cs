using Avalonia;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class GraphViewModel(Graph model) : ViewModelBase, IAdjacencyContext
    {
        public Graph Model { get; init; } = model;
        //public ObservableCollection<IGraphObject> GraphObjects { get; } = [];
        public ObservableCollection<VertexViewModel> Vertices { get; } = [];
        public ObservableCollection<EdgeViewModel> Edges { get; } = [];

        public VertexViewModel AddNewVertex(double x, double y)
        {
            var vertexModel = Model.AddNewVertex();

            var vertexViewModel = new VertexViewModel(this, vertexModel, x, y);

            //GraphObjects.Add(vertexViewModel);
            Vertices.Add(vertexViewModel);

            return vertexViewModel;
        }

        public VertexViewModel AddNewVertex(Point position)
        {
            return AddNewVertex(position.X, position.Y);
        }

        public VertexViewModel AddNewVertexWithId(uint id, double x, double y)
        {
            var vertexModel = Model.AddNewVertexWithId(id);

            var vertexViewModel = new VertexViewModel(this, vertexModel, x, y);

            Vertices.Add(vertexViewModel);

            return vertexViewModel;
        }

        public VertexViewModel AddNewVertexWithId(uint id, Point position)
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

            //GraphObjects.Add(edgeViewModel);
            Edges.Add(edgeViewModel);

            vertexVM1.NotifyEdgeCountChanged();
            vertexVM2.NotifyEdgeCountChanged();
            vertexVM1.NotifyNeighborsChanged();
            vertexVM2.NotifyNeighborsChanged();

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
        }

        public void Clear()
        {
            Edges.Clear();
            Vertices.Clear();
            Model.Clear();
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

        public bool EdgeExists(VertexViewModel vertex1, VertexViewModel vertex2)
        {
            if (vertex1 == null || vertex2 == null)
            {
                return false;
            }

            return Edges.Any(edgeVM =>
                (edgeVM.VertexVM1 == vertex1 && edgeVM.VertexVM2 == vertex2) ||
                (edgeVM.VertexVM1 == vertex2 && edgeVM.VertexVM2 == vertex1)
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