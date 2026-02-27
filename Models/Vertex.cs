using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Models
{
    public class Vertex(int id)
    {
        public int Id { get; init; } = id;
    }
}
