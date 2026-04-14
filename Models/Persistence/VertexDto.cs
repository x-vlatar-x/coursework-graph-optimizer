namespace GraphOptimizer.Models.Persistence
{
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
}
