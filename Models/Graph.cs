using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphOptimizer.Models
{
    public class Graph
    {
        private uint _nextVertexId = 1;

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

        public Vertex AddNewVertexWithId(uint id)
        {
            var vertex = new Vertex(id);
            Vertices.Add(vertex);
            if (id + 1 > _nextVertexId)
            {
                _nextVertexId = id + 1;
            }
            return vertex;
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

        public void Clear()
        {
            Edges.Clear();
            Vertices.Clear();
            _nextVertexId = 1;
        }

        //public void ApplyVertexCover(List<uint> vertexIds)
        //{
        //    var idSet = new HashSet<uint>(vertexIds);

        //    foreach (var vertex in Vertices)
        //    {
        //        vertex.IsInVertexCover = idSet.Contains(vertex.Id);
        //    }
        //}

        //public void ClearVertexCover()
        //{
        //    foreach (var vertex in Vertices)
        //    {
        //        vertex.IsInVertexCover = false;
        //    }
        //}
    }
}
