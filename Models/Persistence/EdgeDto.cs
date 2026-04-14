namespace GraphOptimizer.Models.Persistence
{
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
