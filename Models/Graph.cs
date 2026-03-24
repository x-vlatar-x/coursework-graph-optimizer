using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphOptimizer.Models
{
    public class Graph
    {
        private int _nextVertexId = 1;

        public List<Vertex> Vertices { get; } = [];
        public List<Edge> Edges { get; } = [];

        public Vertex AddNewVertex()
        {
            var vertex = new Vertex(_nextVertexId);
            Vertices.Add(vertex);
            _nextVertexId++;
            return vertex;
        }

        public Edge AddNewEdge(Vertex vertex1, Vertex vertex2)
        {
            var edge = new Edge(vertex1, vertex2);
            Edges.Add(edge);
            return edge;
        }

        public void RemoveVertex(Vertex vertex)
        {
            Vertices.Remove(vertex);

            var edgesToRemove = Edges
                .Where(edge => edge.Vertex1 == vertex || edge.Vertex2 == vertex)
                .ToList();

            foreach (var edge in edgesToRemove)
            {
                RemoveEdge(edge);
            }
        }

        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
        }

        //public ObservableCollection<Vertex> Vertices { get; } = [];
        //public ObservableCollection<Edge> Edges { get; } = [];
    }
}
