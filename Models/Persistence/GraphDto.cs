using System.Collections.Generic;

namespace GraphOptimizer.Models.Persistence
{
    public class GraphDto
    {
        public List<VertexDto> Vertices { get; set; } = [];
        public List<EdgeDto> Edges { get; set; } = [];

        public GraphDto() { }

        public GraphDto(List<VertexDto> vertices, List<EdgeDto> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }
    }

    public class VertexDto
    {
        public uint Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public VertexDto() { }

        public VertexDto(uint id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }

    public class EdgeDto
    {
        public uint VertexId1 { get; set; }
        public uint VertexId2 { get; set; }

        public EdgeDto() { }

        public EdgeDto(uint vertexId1, uint vertexId2)
        {
            VertexId1 = vertexId1;
            VertexId2 = vertexId2;
        }
    }
}