using GraphOptimizer.ViewModels;

namespace GraphOptimizer.Models
{
    public class Vertex(uint id)
    {
        public uint Id { get; init; } = id;
    }
}
