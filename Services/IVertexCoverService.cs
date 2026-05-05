using GraphOptimizer.Enums;
using GraphOptimizer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOptimizer.Services
{
    public interface IVertexCoverService
    {
        //List<uint>
        //public List<uint> Solve(Graph graph, AnalysisMode analysisMode);
        //List<uint> SolveGreedy(Graph graph);
        //List<uint> SolveApprox(Graph graph, bool useRandom = false);
        //List<uint> SolveBacktracking(Graph graph);

        Task<AnalysisResult> Solve(Graph graph, AnalysisMode analysisMode);
        //AnalysisResult SolveGreedy(Graph graph);
        //AnalysisResult SolveApprox(Graph graph, bool useRandom = false);
        //AnalysisResult SolveBacktracking(Graph graph);
    }
}