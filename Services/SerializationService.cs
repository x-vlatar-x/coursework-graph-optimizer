using GraphOptimizer.Models.Persistence;
using GraphOptimizer.ViewModels.GraphCore;
using System.Linq;
using System.Text.Json;

namespace GraphOptimizer.Services
{
    public class SerializationService: ISerializationService
    {
        public string SerializeGraph(GraphViewModel graphVM)
        {
            var graphDto = new GraphDto(
                vertices: graphVM.Vertices.Select(vertexVM => 
                    new VertexDto(vertexVM.Model.Id, vertexVM.X, vertexVM.Y)
                ).ToList(),
                edges: graphVM.Edges.Select(edgeVM => 
                    new EdgeDto(edgeVM.VertexVM1.Model.Id, edgeVM.VertexVM2.Model.Id)
                ).ToList()
            );

            string json = JsonSerializer.Serialize(graphDto, new JsonSerializerOptions { WriteIndented = true });

            return json;
        }

        public GraphDto? DeserializeGraph(string json)
        {
            var graphDto = JsonSerializer.Deserialize<GraphDto>(json);

            return graphDto;
        }
    }
}
