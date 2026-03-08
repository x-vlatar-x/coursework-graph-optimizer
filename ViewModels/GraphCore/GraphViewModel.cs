using Avalonia;
using GraphOptimizer.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public class GraphViewModel(Graph model) : ViewModelBase
    {
        public Graph Model { get; init; } = model;
        //public ObservableCollection<IGraphObject> GraphObjects { get; } = [];
        public ObservableCollection<VertexViewModel> Vertices { get; } = [];
        public ObservableCollection<EdgeViewModel> Edges { get; } = [];

        public VertexViewModel AddNewVertex(double x, double y)
        {
            var vertexModel = Model.AddNewVertex();

            var vertexViewModel = new VertexViewModel(vertexModel, x, y);

            //GraphObjects.Add(vertexViewModel);
            Vertices.Add(vertexViewModel);

            return vertexViewModel;
        }

        public VertexViewModel AddNewVertex(Point position)
        {
            return AddNewVertex(position.X, position.Y);
        }

        public EdgeViewModel? AddNewEdge(VertexViewModel vertex1, VertexViewModel vertex2)
        {
            if (HasEdge(vertex1, vertex2))
            {
                return null;
            }

            var edgeModel = Model.AddNewEdge(vertex1.Model, vertex2.Model);

            var edgeViewModel = new EdgeViewModel(edgeModel, vertex1, vertex2);

            //GraphObjects.Add(edgeViewModel);
            Edges.Add(edgeViewModel);

            return edgeViewModel;
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
        }

        public bool HasEdge(VertexViewModel vertex1, VertexViewModel vertex2)
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
    }
}