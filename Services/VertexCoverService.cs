using Avalonia.Animation;
using GraphOptimizer.Enums;
using GraphOptimizer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphOptimizer.Services
{
    public class VertexCoverService: IVertexCoverService
    {
        private int _greedyOperationsCount = 0;
        private int _approxOperationsCount = 0;
        private int _backtrackingOperationsCount = 0;

        //public List<uint> Solve(Graph graph, AnalysisMode analysisMode)
        public AnalysisResult Solve(Graph graph, AnalysisMode analysisMode)
        {
            return analysisMode switch
            {
                AnalysisMode.Greedy => this.SolveGreedy(graph),
                AnalysisMode.Approx => this.SolveApprox(graph, true),
                AnalysisMode.Backtracking => this.SolveBacktracking(graph),
                _ => new AnalysisResult(AnalysisMode.Greedy, [], 0, 0, 0)
            }; 
        }

        //public List<uint> SolveGreedy(Graph graph)
        public AnalysisResult SolveGreedy(Graph graph)
        {
            _greedyOperationsCount = 0;
            var stopwatch = Stopwatch.StartNew();

            List<uint> cover = new List<uint>();

            var edges = graph.Edges.ToList();

            while (edges.Count > 0)
            {
                var vertexDegree = new Dictionary<uint, int>();

                foreach (var edge in edges)
                {
                    _greedyOperationsCount++;
                    if (!vertexDegree.ContainsKey(edge.Vertex1.Id))
                    {
                        vertexDegree[edge.Vertex1.Id] = 0;
                    }
                    if (!vertexDegree.ContainsKey(edge.Vertex2.Id))
                    {
                        vertexDegree[edge.Vertex2.Id] = 0;
                    }
                    vertexDegree[edge.Vertex1.Id]++;
                    vertexDegree[edge.Vertex2.Id]++;
                }

                _greedyOperationsCount++;
                uint maxDegreeVertexId = vertexDegree.OrderByDescending(v => v.Value).First().Key;
                cover.Add(maxDegreeVertexId);

                var filteredEdges = new List<Edge>();

                foreach (var edge in edges)
                {
                    _greedyOperationsCount++;
                    if (edge.Vertex1.Id != maxDegreeVertexId && edge.Vertex2.Id != maxDegreeVertexId)
                    {
                        filteredEdges.Add(edge);
                    }
                }

                edges = filteredEdges;
            }

            stopwatch.Stop();

            //return cover;
            return new AnalysisResult(AnalysisMode.Greedy, cover, cover.Count, stopwatch.Elapsed.TotalMilliseconds, _greedyOperationsCount);
        }

        //public List<uint> SolveApprox(Graph graph, bool useRandom = false)
        public AnalysisResult SolveApprox(Graph graph, bool useRandom = false)
        {
            _approxOperationsCount = 0;
            var stopwatch = Stopwatch.StartNew();

            List<uint> cover = new List<uint>();

            var edges = graph.Edges.ToList();

            var random = new Random();

            while (edges.Count > 0)
            {
                _approxOperationsCount++;
                int index = useRandom ? random.Next(edges.Count) : 0;

                uint vertexId1 = edges[index].Vertex1.Id;
                uint vertexId2 = edges[index].Vertex2.Id;

                cover.Add(vertexId1);
                cover.Add(vertexId2);

                var filteredEdges = new List<Edge>();

                foreach (var edge in edges)
                {
                    _approxOperationsCount++;
                    if (edge.Vertex1.Id != vertexId1 && edge.Vertex2.Id != vertexId1
                        && edge.Vertex1.Id != vertexId2 && edge.Vertex2.Id != vertexId2)
                    {
                        filteredEdges.Add(edge);
                    }
                }

                edges = filteredEdges;
            }

            stopwatch.Stop();

            //return cover.Distinct().ToList();
            return new AnalysisResult(AnalysisMode.Approx, cover, cover.Count, stopwatch.Elapsed.TotalMilliseconds, _approxOperationsCount);
        }

        //public List<uint> SolveBacktracking(Graph graph)
        public AnalysisResult SolveBacktracking(Graph graph)
        {
            _backtrackingOperationsCount = 0;
            var stopwatch = Stopwatch.StartNew();

            var edges = graph.Edges.ToList();

            List<uint> cover = Backtrack(edges);

            stopwatch.Stop();

            //return cover;
            return new AnalysisResult(AnalysisMode.Backtracking, cover, cover.Count, stopwatch.Elapsed.TotalMilliseconds, _backtrackingOperationsCount);
        }

        //private List<uint> Backtrack(List<Edge> edges)
        private List<uint> Backtrack(List<Edge> edges)
        {
            if (edges.Count == 0)
            {
                return new List<uint>();
            }

            uint vertexId1 = edges[0].Vertex1.Id;
            uint vertexId2 = edges[0].Vertex2.Id;

            _backtrackingOperationsCount += edges.Count;
            var edgesWithoutVertex1 = edges.Where(edge => edge.Vertex1.Id != vertexId1 && edge.Vertex2.Id != vertexId1).ToList();
            var coverWithVertex1 = Backtrack(edgesWithoutVertex1);
            coverWithVertex1.Add(vertexId1);

            _backtrackingOperationsCount += edges.Count;
            var edgesWithoutVertex2 = edges.Where(edge => edge.Vertex1.Id != vertexId2 && edge.Vertex2.Id != vertexId2).ToList();
            var coverWithVertex2 = Backtrack(edgesWithoutVertex2);
            coverWithVertex2.Add(vertexId2);

            _backtrackingOperationsCount++;
            return coverWithVertex1.Count < coverWithVertex2.Count ? coverWithVertex1 : coverWithVertex2;
        }
    }
}