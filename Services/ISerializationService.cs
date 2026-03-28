using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;

namespace GraphOptimizer.Services
{
    public interface ISerializationService
    {
        string SerializeGraph(GraphViewModel graphVM);
        GraphDto? DeserializeGraph(string json);
    }
}
