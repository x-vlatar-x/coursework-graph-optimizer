using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.Models.Persistence
{
    public class ResultDto: ProjectDto
    {
        public override string Type { get; init; } = "Result";
        //public List<VertexDto> Vertices { get; init; } = new List<VertexDto>();
        //public List<EdgeDto> Edges { get; init; } = new List<EdgeDto>();
        public AnalysisResult Result { get; init; }

        public ResultDto() { }

        public ResultDto(List<VertexDto> vertices, List<EdgeDto> edges, AnalysisResult analysisResult)
            :base(vertices, edges)
        {
            //Vertices = vertices;
            //Edges = edges;
            Result = analysisResult;
        }
    }
}
