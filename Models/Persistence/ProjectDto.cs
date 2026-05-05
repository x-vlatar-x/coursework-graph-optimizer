using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GraphOptimizer.Models.Persistence
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
    [JsonDerivedType(typeof(ProjectDto), typeDiscriminator: "Project")]
    [JsonDerivedType(typeof(ResultDto), typeDiscriminator: "Result")]
    public class ProjectDto
    {
        [JsonIgnore]
        public virtual string Type { get; init; } = "Project";
        public List<VertexDto> Vertices { get; set; } = new List<VertexDto>();
        public List<EdgeDto> Edges { get; set; } = new List<EdgeDto>();

        public ProjectDto() { }

        public ProjectDto(List<VertexDto> vertices, List<EdgeDto> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }
    }

    //public class VertexDto
    //{
    //    public uint Id { get; set; }
    //    public double X { get; set; }
    //    public double Y { get; set; }

    //    public VertexDto() { }

    //    public VertexDto(uint id, double x, double y)
    //    {
    //        Id = id;
    //        X = x;
    //        Y = y;
    //    }
    //}

    //public class EdgeDto
    //{
    //    public uint VertexId1 { get; set; }
    //    public uint VertexId2 { get; set; }

    //    public EdgeDto() { }

    //    public EdgeDto(uint vertexId1, uint vertexId2)
    //    {
    //        VertexId1 = vertexId1;
    //        VertexId2 = vertexId2;
    //    }
    //}
}