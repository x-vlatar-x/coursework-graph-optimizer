using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;

namespace GraphOptimizer.Services
{
    public interface ISerializationService
    {
        string SerializeProject(GraphViewModel graphVM);
        string SerializeResult(GraphViewModel graphVM, AnalysisResult analysisResult);

        //ProjectDto? DeserializeProject(string json);
        //ResultDto? DeserializeResult(string json);

        ProjectDto? DeserializeAny(string json);
    }
}
