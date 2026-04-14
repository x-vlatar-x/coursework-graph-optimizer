using Avalonia;
using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;
using System.Threading.Tasks;

namespace GraphOptimizer.Services
{
    public interface IFileService
    {
        Task SaveProjectAsync(Visual visualRoot, GraphViewModel graphVM);
        Task SaveResultAsync(Visual visualRoot, GraphViewModel graphVM, AnalysisResult analysisResult);
        Task<ProjectDto?> LoadProjectAsync(Visual visualRoot);
    }
}
