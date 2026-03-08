using Avalonia;
using GraphOptimizer.ViewModels.GraphCore;
using GraphOptimizer.ViewModels.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace GraphOptimizer.ViewModels
{
    public enum EditorLayoutMode { Freeform, Circular } 
    public enum EditorTool { Move, Vertex, Edge }
    public partial class GraphEditorViewModel: ViewModelBase
    {
        //public ObservableCollection<Vertex> Vertices => MyGraph.Vertices;
        //public ObservableCollection<Edge> Edges => MyGraph.Edges;

        public GraphViewModel GraphVM { get; init; }

        //public MouseState Cursor {  get; init; } = new MouseState();
        //public EditorContext Session { get; init; } = new EditorContext();
        public EditorContext EditorContext { get; init; } = new EditorContext();

        public GraphEditorViewModel(GraphViewModel graphVM)
        {
            GraphVM = graphVM;

            var vertex1 = GraphVM.AddNewVertex(100, 50);
            var vertex2 = GraphVM.AddNewVertex(50, 100);
            var edge1 = GraphVM.AddNewEdge(vertex1, vertex2);
            var vertex3 = GraphVM.AddNewVertex(140, 90);
            var vertex4 = GraphVM.AddNewVertex(100, 200);

            var edge2 = GraphVM.AddNewEdge(vertex2, vertex3);
            var edge3 = GraphVM.AddNewEdge(vertex3, vertex4);
        }

        private EditorTool _selectedTool = EditorTool.Move;
        public EditorTool SelectedTool
        {
            get => _selectedTool;
            set => SetProperty(ref _selectedTool, value);
        }

        private EditorLayoutMode _currentLayoutMode = EditorLayoutMode.Freeform;
        public EditorLayoutMode CurrentLayoutMode
        {
            get => _currentLayoutMode;
            set => SetProperty(ref _currentLayoutMode, value);
        }

        public void SetSelectedTool(EditorTool tool)
        {
            //if (SelectedTool != tool && Session.SelectedVertexVM != null) {
            //    Session.SelectedVertexVM.IsSelected = false;
            //    Session.SelectedVertexVM = null;
            //}
            //if (SelectedTool != tool && Session.SelectedEdgeVM != null)
            //{
            //    Session.SelectedEdgeVM.IsSelected = false;
            //    Session.SelectedEdgeVM = null;
            //}
            SelectedTool = tool;
            EditorContext.StopSelecting();
            EditorContext.StopHovering();
            EditorContext.ClearSelection();
        }

        public void SetLayoutMode(EditorLayoutMode mode)
        {
            CurrentLayoutMode = mode;
        }

        public void HandlePointerEntered()
        {
            //Cursor.IsInCanvas = true;
            EditorContext.IsMouseOverCanvas = true;
        }

        public void HandlePointerExited()
        {
            //Cursor.IsInCanvas = false;
            EditorContext.IsMouseOverCanvas = false;
        }

        public void HandlePointerMoved(Point position, Size canvasSize)
        {
            EditorContext.MousePosition = position;

            switch (SelectedTool)
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
            switch (SelectedTool)
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
            //int vertexSize = 20;
            //switch (SelectedTool)
            //{
            //    case EditorTool.Move:
            //    {
            //        var pressedVertexVM = GeometryHelper.FindVertexAtPoint(Graph, position);

            //        var pressedEdgeVM = pressedVertexVM == null 
            //            ? GeometryHelper.FindEdgeAtPoint(Graph, position) 
            //            : null;

            //        if (Session.SelectedVertexVM != null && Session.SelectedVertexVM != pressedVertexVM)
            //        {
            //            Session.SelectedVertexVM.IsSelected = false;
            //            Session.SelectedVertexVM = null;
            //        }

            //        if (Session.SelectedEdgeVM != null && Session.SelectedEdgeVM != pressedEdgeVM)
            //        {
            //            Session.SelectedEdgeVM.IsSelected = false;
            //            Session.SelectedEdgeVM = null;
            //        }

            //        if (pressedVertexVM != null)
            //        {
            //            Session.SelectedVertexVM = pressedVertexVM;
            //            Session.SelectedVertexVM.IsSelected = true;
            //            Session.StartDragging(pressedVertexVM, position);
            //        }
            //        else if (pressedEdgeVM != null)
            //        {
            //            Session.SelectedEdgeVM = pressedEdgeVM;
            //            Session.SelectedEdgeVM.IsSelected = true;
            //        }
            //        break;
            //    }
            //    case EditorTool.Vertex:
            //    {
            //        Graph.AddNewVertex(position.X, position.Y);
            //        SelectedTool = EditorTool.Move;
            //        break;
            //    }
            //    case EditorTool.Edge:
            //    { 
            //        var pressedVertexVM = GeometryHelper.FindVertexAtPoint(Graph, position);
                    
            //        if (Session.SelectedVertexVM != null && Session.SelectedVertexVM != pressedVertexVM)
            //        {
            //            Session.SelectedVertexVM.IsSelected = false;
            //            Session.SelectedVertexVM = null;
            //        }

            //            if (pressedVertexVM != null)
            //        {
            //            if (Session.SelectedVertexVM != null)
            //            {
            //                Session.SelectedVertexVM.IsSelected = false;
            //            }
            //            Session.SelectedVertexVM = pressedVertexVM;
            //            Session.SelectedVertexVM.IsSelected = true;
            //            Session.IsEdgeConnecting = true;
            //        }
            //        break;
            //    }
            //    default:
            //    {
            //        break;
            //    }
            //}
        }

        public void HandlePointerReleased(Point position)
        {
            switch (SelectedTool)
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
            //switch (SelectedTool)
            //{
            //    case EditorTool.Move:
            //        Session.StopDragging();
            //        break;
            //    case EditorTool.Edge:
            //        if (Session.SelectedVertexVM != null)
            //        {
            //            var pressedVertexVM = GeometryHelper.FindVertexAtPoint(Graph, position);
            //            if (pressedVertexVM != null)
            //            {
            //                Graph.AddNewEdge(Session.SelectedVertexVM, pressedVertexVM);
            //            }
            //            Session.SelectedVertexVM.IsSelected = false;
            //            Session.SelectedVertexVM = null;
            //            Session.IsEdgeConnecting = false;
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
