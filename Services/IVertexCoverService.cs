using GraphOptimizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
