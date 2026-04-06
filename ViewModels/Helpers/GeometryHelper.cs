using Avalonia;
using GraphOptimizer.Interfaces;
using GraphOptimizer.Models;
using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;
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

        public static IGraphObject? FindObjectAtPoint(GraphViewModel graphVM, Point mousePosition)
        {
            var foundVertex = FindVertexAtPoint(graphVM, mousePosition);
            return foundVertex != null ? foundVertex : FindEdgeAtPoint(graphVM, mousePosition);
        }

        public static List<IGraphObject> FindObjectsInRect(GraphViewModel graphVM, Point startPoint, Point endPoint)
        {
            List<IGraphObject> objectsVM = [];

            double left = Math.Min(startPoint.X, endPoint.X);
            double right = Math.Max(startPoint.X, endPoint.X);
            double top = Math.Min(startPoint.Y, endPoint.Y);
            double bottom = Math.Max(startPoint.Y, endPoint.Y);

            foreach (var vertexVM in graphVM.Vertices)
            {
                if (vertexVM.X >= left && vertexVM.X <= right
                    && vertexVM.Y >= top && vertexVM.Y <= bottom)
                {
                    objectsVM.Add(vertexVM);
                }
            }

            foreach (var edgeVM in graphVM.Edges)
            {
                if ((edgeVM.VertexVM1.X >= left && edgeVM.VertexVM1.X <= right
                    && edgeVM.VertexVM1.Y >= top && edgeVM.VertexVM1.Y <= bottom)
                    && (edgeVM.VertexVM2.X >= left && edgeVM.VertexVM2.X <= right
                    && edgeVM.VertexVM2.Y >= top && edgeVM.VertexVM2.Y <= bottom))
                {
                    objectsVM.Add(edgeVM);
                }
            }

            return objectsVM;
        }

        public static Point FindFreePosition(GraphViewModel graphVM, Rect canvasBounds)
        {
            double vertexRadius = 12;
            double minDistance = vertexRadius * 3;

            double radius = 0;
            double stepRadius = 10;

            double centerX = canvasBounds.Width / 2;
            double centerY = canvasBounds.Height / 2;

            double x;
            double y;

            while (radius < Math.Min(canvasBounds.Width, canvasBounds.Height) / 2)
            {
                double circumference = 2 * Math.PI * radius;
                int pointsInLayer = (radius == 0) ? 1 : (int)(circumference / minDistance);

                double stepTheta = (pointsInLayer > 0) ? (2 * Math.PI / pointsInLayer) : 2 * Math.PI;

                double theta = 0;
                while (theta < 2 * Math.PI)
                {
                    x = centerX + radius * Math.Cos(theta);
                    y = centerY + radius * Math.Sin(theta);

                    if (IsPositionFree(graphVM, new Point(x, y), minDistance))
                    {
                        return new Point(x, y);
                    }

                    theta += stepTheta;
                }

                radius += stepRadius;
            }

            return new Point(centerX, centerY);
        }

        public static bool IsPositionFree(GraphViewModel graphVM, Point position, double radius)
        {
            foreach (var vertexVM in graphVM.Vertices)
            {
                double dx = vertexVM.X - position.X;
                double dy = vertexVM.Y - position.Y;
                if ((dx * dx) + (dy * dy) <= radius * radius)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
