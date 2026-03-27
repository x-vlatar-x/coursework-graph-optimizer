using Avalonia;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System;
using System.Diagnostics;

namespace GraphOptimizer.ViewModels
{
    public class GraphTableViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }
        public EditorContext EditorContext { get; init; }

        public GraphTableViewModel(GraphViewModel graphVM, EditorContext editorContext) 
        {
            GraphVM = graphVM;
            EditorContext = editorContext;
        }

        public void HandleExpandButtonClick(VertexViewModel vertexVM)
        {
            vertexVM.ToggleExpansion();
        }

        public void HandleVertexAddPressed()
        {
            if (EditorContext.CurrentLayoutMode == EditorLayoutMode.Freeform)
            {
                var vertexPosition = GeometryHelper.FindFreePosition(GraphVM, EditorContext.CanvasBounds);
                
                GraphVM.AddNewVertex(vertexPosition);
            }
        }
        public void HandleNeighborAddPressed(VertexViewModel vertexVM)
        {
            if (vertexVM.IsIdValid && uint.TryParse(vertexVM.InputNeighborId, out uint id))
            {
                GraphVM.AddNewEdge(vertexVM.Model.Id, id);
            }
        }

        public void HandleVertexDeletePressed(VertexViewModel vertexVM)
        {
            GraphVM.RemoveVertex(vertexVM);
        }

        public void HandleNeighborDeletePressed(VertexViewModel vertexVM, VertexViewModel neighborVM)
        {
            GraphVM.RemoveEdge(vertexVM, neighborVM);
        }
    }
}
