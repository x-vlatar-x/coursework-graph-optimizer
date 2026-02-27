namespace GraphOptimizer.Models
{
    public class Edge(Vertex vertex1, Vertex vertex2)
    {
        public Vertex Vertex1 { set; get; } = vertex1;
        public Vertex Vertex2 { set; get; } = vertex2;
    }
}
