using Avalonia;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.ViewModels.GraphCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphOptimizer.ViewModels.Helpers
{
    public class EditorContext : ViewModelBase
    {
        // Editor state
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

        private Rect _canvasBounds;
        public Rect CanvasBounds
        {
            get => _canvasBounds;
            set => _canvasBounds = value;
        }

        // Mouse state
        private Point _mousePosition;
        public Point MousePosition
        {
            get => _mousePosition;
            set
            {
                if (IsDragging)
                {
                    Offset = new Point(value.X - _mousePosition.X, value.Y - _mousePosition.Y);
                }
                SetProperty(ref _mousePosition, value);
                if (IsSelecting)
                {
                    SelectionRect = new Rect(StartPoint, value).Normalize();
                }
            }
        }

        private Point _startPoint = new Point(0, 0);
        public Point StartPoint
        {
            get => _startPoint;
            set => SetProperty(ref _startPoint, value);
        }

        private Point _endPoint = new Point(0, 0);
        public Point EndPoint
        {
            get => _endPoint;
            set => SetProperty(ref _endPoint, value);
        }

        public Point Offset { get; set; } = new Point(0, 0);

        // Objects state
        public IGraphObject? HoveredObject { get; set; }
        public HashSet<IGraphObject> SelectedObjects { get; } = [];
        public HashSet<VertexViewModel> DraggedVertices { get; } = [];

        public VertexViewModel? ActiveVertex { get; set; } = null;

        private Rect _selectionRect;
        public Rect SelectionRect
        {
            get => _selectionRect;
            set => SetProperty(ref _selectionRect, value);
        }

        // Flags
        private bool _isMouseOverCanvas;
        public bool IsMouseOverCanvas
        {
            get => _isMouseOverCanvas;
            set => SetProperty(ref _isMouseOverCanvas, value);
        }

        private bool _isSelecting;
        public bool IsSelecting
        {
            get => _isSelecting;
            set => SetProperty(ref _isSelecting, value);
        }

        private bool _isDragging;
        public bool IsDragging
        {
            get => _isDragging;
            set => SetProperty(ref _isDragging, value);
        }

        public bool WasDragged { get; set; } = false;

        private bool _isConnecting;
        public bool IsConnecting
        {
            get => _isConnecting;
            set => SetProperty(ref _isConnecting, value);
        }

        // METHODS
        public void StartSelecting(Point mousePosition)
        {
            StartPoint = mousePosition;
            IsSelecting = true;
            SelectionRect = new Rect(StartPoint, mousePosition).Normalize();
        }

        public void StopSelecting()
        {
            IsSelecting = false;
        }

        public void StartHovering(IGraphObject objectVM)
        {
            if (HoveredObject != null)
            {
                HoveredObject.IsHovered = false;
            }
            HoveredObject = objectVM;
            HoveredObject.IsHovered = true;
        }

        public void StopHovering()
        {
            if (HoveredObject != null)
            {
                HoveredObject.IsHovered = false;
                HoveredObject = null;
            }
        }

        public void Select(IGraphObject objectVM)
        {
            if (SelectedObjects.Add(objectVM))
            {
                objectVM.IsSelected = true;

                if (objectVM is VertexViewModel vertexVM)
                {
                    DraggedVertices.Add(vertexVM);
                }
                else if (objectVM is EdgeViewModel edge)
                {
                    DraggedVertices.Add(edge.VertexVM1);
                    DraggedVertices.Add(edge.VertexVM2);
                }
            }
        }

        public void Select(IEnumerable<IGraphObject> objectsVM)
        {
            foreach (var objectVM in objectsVM)
            {
                Select(objectVM);
            }
        }

        public void Deselect(IGraphObject objectVM)
        {
            objectVM.IsSelected = false;
            SelectedObjects.Remove(objectVM);

            if (objectVM is VertexViewModel vertexVM)
            {
                DraggedVertices.Remove(vertexVM);
            }
            else if (objectVM is EdgeViewModel edge)
            {
                DraggedVertices.Remove(edge.VertexVM1);
                DraggedVertices.Remove(edge.VertexVM2);
            }
        }

        public void ClearSelection()
        {
            foreach (var objectVM in SelectedObjects)
            {
                objectVM.IsSelected = false;
            }
            SelectedObjects.Clear();
            DraggedVertices.Clear();
        }

        public void StartDragging(Point mousePosition)
        {
            IsDragging = true;
            StartPoint = mousePosition;
        }

        public void Drag(Point mousePosition)
        {
            if (Offset.X != 0 || Offset.Y != 0)
            {
                WasDragged = true;
            }

            foreach (var vertexVM in DraggedVertices)
            {
                vertexVM.X += Offset.X;
                vertexVM.Y += Offset.Y;
            }
        }

        public void StopDragging()
        {
            IsDragging = false;
            WasDragged = false;
            Offset = new Point(0, 0);
            StartPoint = new Point(0, 0);
        }

        public void StartConnecting(VertexViewModel vertexVM)
        {
            IsConnecting = true;
            StartPoint = new Point(vertexVM.X, vertexVM.Y);
            ActiveVertex = vertexVM;
        }

        public void StopConnecting()
        {
            IsConnecting = false;
            StartPoint = new Point(0, 0);
            ActiveVertex = null;
        }
    }
}