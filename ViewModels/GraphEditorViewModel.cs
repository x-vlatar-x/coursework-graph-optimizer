using Avalonia;
using GraphOptimizer.Enums;
using GraphOptimizer.Interfaces;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace GraphOptimizer.ViewModels
{
    //public enum EditorLayoutMode { Freeform, Circular }
    //public enum EditorTool { Move, Vertex, Edge }
    public partial class GraphEditorViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public IAppState AppState { get; init; }

        public EditorContext EditorContext { get; init; }

        public GraphEditorViewModel(GraphViewModel graphVM, IAppState appState, EditorContext editorContext)
        {
            GraphVM = graphVM;
            AppState = appState;
            EditorContext = editorContext;

            //var vertex1 = GraphVM.AddNewVertex(100, 50);
            //var vertex2 = GraphVM.AddNewVertex(50, 100);
            //var edge1 = GraphVM.AddNewEdge(vertex1, vertex2);
            //var vertex3 = GraphVM.AddNewVertex(140, 90);
            //var vertex4 = GraphVM.AddNewVertex(100, 200);

            //var edge2 = GraphVM.AddNewEdge(vertex2, vertex3);
            //var edge3 = GraphVM.AddNewEdge(vertex3, vertex4);

            //var vertex5 = GraphVM.AddNewVertex(250, 50);
            //var vertex6 = GraphVM.AddNewVertex(350, 50);
            //var vertex7 = GraphVM.AddNewVertex(450, 50);

            //var vertex8 = GraphVM.AddNewVertex(250, 150);
            //var vertex9 = GraphVM.AddNewVertex(350, 150);
            //var vertex10 = GraphVM.AddNewVertex(450, 150);

            //var vertex11 = GraphVM.AddNewVertex(250, 250);
            //var vertex12 = GraphVM.AddNewVertex(350, 250);
            //var vertex13 = GraphVM.AddNewVertex(450, 250);

            //var vertex14 = GraphVM.AddNewVertex(50, 350);
            //var vertex15 = GraphVM.AddNewVertex(150, 350);
            //var vertex16 = GraphVM.AddNewVertex(250, 350);

            //var vertex17 = GraphVM.AddNewVertex(350, 350);
            //var vertex18 = GraphVM.AddNewVertex(450, 350);

            //var vertex19 = GraphVM.AddNewVertex(100, 450);
            //var vertex20 = GraphVM.AddNewVertex(250, 450);
            //var vertex21 = GraphVM.AddNewVertex(400, 450);

            //GraphVM.AddNewEdge(vertex3, vertex5);
            //GraphVM.AddNewEdge(vertex3, vertex6);
            //GraphVM.AddNewEdge(vertex3, vertex7);
            //GraphVM.AddNewEdge(vertex3, vertex8);
            //GraphVM.AddNewEdge(vertex3, vertex9);
            //GraphVM.AddNewEdge(vertex3, vertex10);
            //GraphVM.AddNewEdge(vertex3, vertex11);
            //GraphVM.AddNewEdge(vertex3, vertex12);
            //GraphVM.AddNewEdge(vertex3, vertex13);
            //GraphVM.AddNewEdge(vertex3, vertex14);
            //GraphVM.AddNewEdge(vertex3, vertex15);
            //GraphVM.AddNewEdge(vertex3, vertex16);
            //GraphVM.AddNewEdge(vertex3, vertex17);
            //GraphVM.AddNewEdge(vertex3, vertex18);
            //GraphVM.AddNewEdge(vertex3, vertex19);
            //GraphVM.AddNewEdge(vertex3, vertex20);
            //GraphVM.AddNewEdge(vertex3, vertex21);

            var vertex1 = GraphVM.AddNewVertex(80, 60);
            var vertex2 = GraphVM.AddNewVertex(45, 110);
            var vertex3 = GraphVM.AddNewVertex(130, 95);
            var vertex4 = GraphVM.AddNewVertex(90, 160);
            var vertex5 = GraphVM.AddNewVertex(210, 75);
            var vertex6 = GraphVM.AddNewVertex(280, 55);
            var vertex7 = GraphVM.AddNewVertex(360, 85);
            var vertex8 = GraphVM.AddNewVertex(190, 155);
            var vertex9 = GraphVM.AddNewVertex(275, 140);
            var vertex10 = GraphVM.AddNewVertex(380, 165);
            var vertex11 = GraphVM.AddNewVertex(160, 240);
            var vertex12 = GraphVM.AddNewVertex(250, 260);
            var vertex13 = GraphVM.AddNewVertex(340, 230);
            var vertex14 = GraphVM.AddNewVertex(70, 280);
            var vertex15 = GraphVM.AddNewVertex(145, 330);
            var vertex16 = GraphVM.AddNewVertex(225, 310);
            var vertex17 = GraphVM.AddNewVertex(310, 345);
            var vertex18 = GraphVM.AddNewVertex(390, 300);
            var vertex19 = GraphVM.AddNewVertex(120, 410);
            var vertex20 = GraphVM.AddNewVertex(240, 400);
            var vertex21 = GraphVM.AddNewVertex(350, 420);

            GraphVM.AddNewEdge(vertex1, vertex2);
            GraphVM.AddNewEdge(vertex2, vertex3);
            GraphVM.AddNewEdge(vertex3, vertex4);
            GraphVM.AddNewEdge(vertex4, vertex1);
            GraphVM.AddNewEdge(vertex3, vertex5);
            GraphVM.AddNewEdge(vertex5, vertex6);
            GraphVM.AddNewEdge(vertex6, vertex9);
            GraphVM.AddNewEdge(vertex9, vertex8);
            GraphVM.AddNewEdge(vertex8, vertex3);
            GraphVM.AddNewEdge(vertex7, vertex10);
            GraphVM.AddNewEdge(vertex10, vertex13);
            GraphVM.AddNewEdge(vertex13, vertex18);
            GraphVM.AddNewEdge(vertex18, vertex17);
            GraphVM.AddNewEdge(vertex4, vertex14);
            GraphVM.AddNewEdge(vertex14, vertex15);
            GraphVM.AddNewEdge(vertex15, vertex19);
            GraphVM.AddNewEdge(vertex19, vertex20);
            GraphVM.AddNewEdge(vertex20, vertex16);
            GraphVM.AddNewEdge(vertex16, vertex12);
            GraphVM.AddNewEdge(vertex12, vertex11);
            GraphVM.AddNewEdge(vertex9, vertex12);
            GraphVM.AddNewEdge(vertex10, vertex21);
            GraphVM.AddNewEdge(vertex21, vertex17);
            GraphVM.AddNewEdge(vertex5, vertex8);
        }

        public void SetSelectedTool(EditorTool tool)
        {
            EditorContext.SelectedTool = tool;
            EditorContext.StopSelecting();
            EditorContext.StopHovering();
            EditorContext.ClearSelection();
        }

        public void SetLayoutMode(EditorLayoutMode mode)
        {
            EditorContext.CurrentLayoutMode = mode;
        }

        public void HandlePointerEntered()
        {
            EditorContext.IsMouseOverCanvas = true;
        }

        public void HandlePointerExited()
        {
            EditorContext.IsMouseOverCanvas = false;
        }

        public void HandlePointerMoved(Point position, Size canvasSize)
        {
            EditorContext.MousePosition = position;

            switch (EditorContext.SelectedTool)
            {
                case EditorTool.Move:
                    if (!EditorContext.IsSelecting)
                    {
                        var hoveredObjectVM = GeometryHelper.FindObjectAtPoint(GraphVM, position);
                        if (hoveredObjectVM != null)
                        {
                            EditorContext.StartHovering(hoveredObjectVM);
                        } else
                        {
                            EditorContext.StopHovering();
                        }
                    }
                    if (EditorContext.SelectedObjects.Count > 0 && EditorContext.IsDragging)
                    {
                        EditorContext.Drag(position);
                    }
                    break;
                case EditorTool.Vertex:
                    break;
                case EditorTool.Edge:
                    if (AppState.IsAnalysisActive)
                    {
                        break;
                    }

                    var hoveredVertexVM = GeometryHelper.FindVertexAtPoint(GraphVM, position);
                    if (hoveredVertexVM != null)
                    {
                        EditorContext.StartHovering(hoveredVertexVM);
                    } else
                    {
                        EditorContext.StopHovering();
                    }
                    break;
                default:
                    break;
            }
        }

        public void HandleLeftClick(Point position)
        {
            if (AppState.IsAnalysisActive)
            {
                return;
            }

            switch (EditorContext.SelectedTool)
            {
                case EditorTool.Move:
                    {
                        var pressedObjectVM = GeometryHelper.FindObjectAtPoint(GraphVM, position);
                        if (pressedObjectVM != null)
                        {
                            if (pressedObjectVM.IsSelected)
                            {
                                EditorContext.StartDragging(position);
                            }
                            else
                            {
                                EditorContext.ClearSelection();
                                EditorContext.Select(pressedObjectVM);
                            }
                        } 
                        else
                        {
                            EditorContext.ClearSelection();
                            EditorContext.StartSelecting(position);
                        }
                    }
                    break;
                case EditorTool.Vertex:
                    {
                        GraphVM.AddNewVertex(position);
                        SetSelectedTool(EditorTool.Move);
                    }
                    break;
                case EditorTool.Edge:
                    {
                        if (EditorContext.ActiveVertex == null)
                        {
                            var pressedVertexVM = GeometryHelper.FindVertexAtPoint(GraphVM, position);
                            if (pressedVertexVM != null)
                            {
                                EditorContext.StartConnecting(pressedVertexVM);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void HandlePointerReleased(Point position)
        {
            if (AppState.IsAnalysisActive)
            {
                return;
            }

            switch (EditorContext.SelectedTool)
            {
                case EditorTool.Move:
                    {
                        if (EditorContext.IsSelecting)
                        {
                            EditorContext.StopSelecting();
                            EditorContext.Select(GeometryHelper.FindObjectsInRect(GraphVM, EditorContext.StartPoint, EditorContext.MousePosition));
                        }
                        
                        var pressedObjectVM = GeometryHelper.FindObjectAtPoint(GraphVM, position);
                        if (EditorContext.IsDragging)
                        {
                            if (!EditorContext.WasDragged && pressedObjectVM != null)
                            {
                                EditorContext.ClearSelection();
                                EditorContext.Select(pressedObjectVM);
                            }
                            EditorContext.StopDragging();
                        } else if (pressedObjectVM != null)
                        {
                            EditorContext.Select(pressedObjectVM);
                        }
                    }
                    break;
                case EditorTool.Vertex:
                    break;
                case EditorTool.Edge:
                    {
                        if (EditorContext.IsConnecting && EditorContext.ActiveVertex != null)
                        {
                            var pressedVertexVM = GeometryHelper.FindVertexAtPoint(GraphVM, position);
                            if (pressedVertexVM != null)
                            {
                                GraphVM.AddNewEdge(EditorContext.ActiveVertex, pressedVertexVM);
                            }
                            EditorContext.StopConnecting();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
