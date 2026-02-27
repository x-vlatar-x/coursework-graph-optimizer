using Avalonia;
using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace GraphOptimizer.ViewModels.Helpers
{
    public static class GeometryHelper
    {
        public static VertexViewModel? FindVertexAtPoint(GraphViewModel graphVM, Point mousePosition)
        {
            return graphVM.Vertices.LastOrDefault(vertex =>
            {
                //Math.Pow(vertex.X - mousePosition.X, 2) + Math.Pow(vertex.Y - mousePosition.Y, 2) <= Math.Pow(12, 2);
                double dx = vertex.X - mousePosition.X;
                double dy = vertex.Y - mousePosition.Y;

                return (dx * dx) + (dy * dy) <= 12 * 12;
            });
        }
        
        public static EdgeViewModel? FindEdgeAtPoint(GraphViewModel graphVM, Point mousePosition)
        {
            double tolerance = 4.0;
            double toleranceSq = tolerance * tolerance;

            return graphVM.Edges.LastOrDefault(edge =>
            {
                if (mousePosition.X < Math.Min(edge.StartPoint.X, edge.EndPoint.X) - tolerance ||
                    mousePosition.X > Math.Max(edge.StartPoint.X, edge.EndPoint.X) + tolerance ||
                    mousePosition.Y < Math.Min(edge.StartPoint.Y, edge.EndPoint.Y) - tolerance ||
                    mousePosition.Y > Math.Max(edge.StartPoint.Y, edge.EndPoint.Y) + tolerance)
                {
                    return false;
                }

                double dx = edge.EndPoint.X - edge.StartPoint.X;
                double dy = edge.EndPoint.Y - edge.StartPoint.Y;
                double lineLenSq = dx * dx + dy * dy;

                if (lineLenSq == 0)
                {
                    return false;
                }

                double t = ((mousePosition.X - edge.StartPoint.X) * dx +
                            (mousePosition.Y - edge.StartPoint.Y) * dy) / lineLenSq;

                t = Math.Clamp(t, 0, 1);

                double closestX = edge.StartPoint.X + t * dx;
                double closestY = edge.StartPoint.Y + t * dy;

                double dX = mousePosition.X - closestX;
                double dY = mousePosition.Y - closestY;

                return (dX * dX + dY * dY) <= toleranceSq;
            });
        }
    }
}
