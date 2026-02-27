using Avalonia;
using Avalonia.Controls.Documents;
using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;

namespace GraphOptimizer.ViewModels.Helpers
{
    public class EditorContext: ViewModelBase
    {
        //private VertexViewModel? _selectedVertexVM = null;
        //public VertexViewModel? SelectedVertexVM
        //{
        //    get => _selectedVertexVM;
        //    set => SetProperty(ref _selectedVertexVM, value);
        //}

        //private EdgeViewModel? _selectedEdgeVM = null;
        //public EdgeViewModel? SelectedEdgeVM
        //{
        //    get => _selectedEdgeVM;
        //    set => SetProperty(ref _selectedEdgeVM, value);
        //}

        //private Point _offset;

        //private VertexViewModel? _draggedVertexVM;
        //public VertexViewModel? DraggedVertexVM
        //{
        //    get => _draggedVertexVM;
        //    set => SetProperty(ref _draggedVertexVM, value);
        //}

        //public void StartDragging(VertexViewModel vertexVM, Point mousePosition)
        //{
        //    _draggedVertexVM = vertexVM;
        //    _offset = new Point(vertexVM.X - mousePosition.X, vertexVM.Y - mousePosition.Y);
        //}

        //public void UpdateDrag(Point mousePosition, Size canvasSize) {
        //    //if (DraggedVertexVM != null) {

        //    if (DraggedVertexVM == null)
        //    {
        //        return;
        //    }

        //    DraggedVertexVM.X = Math.Clamp(mousePosition.X + _offset.X, 0, canvasSize.Width);
        //    DraggedVertexVM.Y = Math.Clamp(mousePosition.Y + _offset.Y, 0, canvasSize.Height);
        //}

        //public void StopDragging()
        //{
        //    _draggedVertexVM = null;
        //}

        //private bool _isEdgeConnecting = false;
        //public bool IsEdgeConnecting { 
        //    get => _isEdgeConnecting; 
        //    set => SetProperty(ref _isEdgeConnecting, value);
        //}

        //// Вспомогательное свойство для XAML
        //public bool IsDraggingEdge => FirstSelectedVertex != null;
        //public bool IsDraggingVertex => DraggedVertex != null;

        // Mouse state
        private Point _mousePosition;
        public Point MousePosition
        {
            get => _mousePosition;
            set => SetProperty(ref _mousePosition, value);
        }

        private Point _startPoint;
        public Point StartPoint
        {
            get => _startPoint;
            set => SetProperty(ref _startPoint, value);
        }

        private Point _endPoint;
        public Point EndPoint
        {
            get => _endPoint;
            set => SetProperty(ref _endPoint, value);
        }

        // Objects state
        public IGraphObject? HoveredObject { get; set; }
        public List<IGraphObject> SelectedObjects { get; } = [];

        public VertexViewModel? ActiveVertex { get; set; } = null;

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

        private bool _isConnecting;
        public bool IsConnecting
        {
            get => IsConnecting;
            set => SetProperty(ref _isConnecting, value);
        }

        // METHODS
        public void Select(IGraphObject objectVM)
        {
            if (!SelectedObjects.Contains(objectVM))
            {
                objectVM.IsSelected = true;
                SelectedObjects.Add(objectVM);
            }
        }

        public void Deselect(IGraphObject objectVM)
        {
            objectVM.IsSelected = false;
            SelectedObjects.Remove(objectVM);
        }

        public void ClearSelection()
        {
            foreach (var objectVM in SelectedObjects)
            {
                objectVM.IsSelected = false;
            }
            SelectedObjects.Clear();
        }
    }
}
