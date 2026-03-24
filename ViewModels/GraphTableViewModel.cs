using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class GraphTableViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public GraphTableViewModel(GraphViewModel graphVM) 
        {
            GraphVM = graphVM;
        }

        public void HandleExpandButtonClick(VertexViewModel vertexVM)
        {
            vertexVM.ToggleExpansion();
        }

        public void HandleNeighborPressed(VertexViewModel vertexVM, VertexViewModel neighborVM)
        {
            GraphVM.RemoveEdge(vertexVM, neighborVM);
        }
    }
}
