using GraphOptimizer.Models;
using System.Collections.Generic;

namespace GraphOptimizer.Services
{
    public interface IVertexCoverService
    {
        //List<uint>
        List<uint> SolveGreedy(Graph graph);
        List<uint> SolveApprox(Graph graph, bool useRandom = false);
        List<uint> SolveBacktracking(Graph graph);
    }
}