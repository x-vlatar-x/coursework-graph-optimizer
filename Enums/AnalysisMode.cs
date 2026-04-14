using System.ComponentModel;

namespace GraphOptimizer.Enums
{
    public enum AnalysisMode {
        [Description("Жадібний метод")]
        Greedy, 

        [Description("Метод Approx-Vertex-Cover")]
        Approx,

        [Description("Пошук з поверненням")]
        Backtracking
    }
}
