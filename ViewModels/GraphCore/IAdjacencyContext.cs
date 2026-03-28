using System.Collections.Generic;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public interface IAdjacencyContext
    {
        IEnumerable<EdgeViewModel> GetEdgesForVertex(VertexViewModel vertexVM);
        IEnumerable<VertexViewModel> GetNeighborsForVertex(VertexViewModel vertexVM);
        bool VertexExists(uint vertexId);
        bool EdgeExists(uint vertexId1, uint vertexId2);
    }
}
