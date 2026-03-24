using System.Collections.Generic;

namespace GraphOptimizer.ViewModels.GraphCore
{
    public interface IAdjacencyContext
    {
        IEnumerable<EdgeViewModel> GetEdgesForVertex(VertexViewModel vertexVM);

        IEnumerable<VertexViewModel> GetNeighborsForVertex(VertexViewModel vertexVM);
    }
}
