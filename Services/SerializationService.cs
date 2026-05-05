using GraphOptimizer.Models;
using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;
using System.Linq;
using System.Text.Json;

namespace GraphOptimizer.Services
{
    public class SerializationService: ISerializationService
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public string SerializeProject(GraphViewModel graphVM)
        {
            var projectDto = new ProjectDto(
                vertices: graphVM.Vertices.Select(vertexVM => 
                    new VertexDto(vertexVM.Model.Id, vertexVM.X, vertexVM.Y)
                ).ToList(),
                edges: graphVM.Edges.Select(edgeVM => 
                    new EdgeDto(edgeVM.VertexVM1.Model.Id, edgeVM.VertexVM2.Model.Id)
                ).ToList()
            );

            string json = JsonSerializer.Serialize(projectDto, _options);

            return json;
        }

        public string SerializeResult(GraphViewModel graphVM, AnalysisResult analysisResult)
        {
            var resultDto = new ResultDto(
                vertices: graphVM.Vertices.Select(vertexVM =>
                    new VertexDto(vertexVM.Model.Id, vertexVM.X, vertexVM.Y)
                ).ToList(),
                edges: graphVM.Edges.Select(edgeVM =>
                    new EdgeDto(edgeVM.VertexVM1.Model.Id, edgeVM.VertexVM2.Model.Id)
                ).ToList(),
                analysisResult: analysisResult
            );

            string json = JsonSerializer.Serialize(resultDto, _options);

            return json;
        }

        public ProjectDto? DeserializeAny(string json)
        {
            return JsonSerializer.Deserialize<ProjectDto>(json);
        }
    }
}
