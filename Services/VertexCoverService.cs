using GraphOptimizer.Enums;
using GraphOptimizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphOptimizer.Services
{
    public class VertexCoverService: IVertexCoverService
    {
        public List<uint> Solve(Graph graph, AnalysisMode analysisMode)
        {
            return analysisMode switch
            {
                AnalysisMode.Greedy => this.SolveGreedy(graph),
                AnalysisMode.Approx => this.SolveApprox(graph),
                AnalysisMode.Backtracking => this.SolveBacktracking(graph),
                _ => new List<uint>()
            }; 
        }

        public List<uint> SolveGreedy(Graph graph)
        {
            List<uint> cover = new List<uint>();

            var edges = graph.Edges.ToList();

            while (edges.Count > 0)
            {
                var vertexDegree = new Dictionary<uint, int>();

                foreach (var edge in edges)
                {
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

                uint maxDegreeVertexId = vertexDegree.OrderByDescending(v => v.Value).First().Key;
                cover.Add(maxDegreeVertexId);

                var filteredEdges = new List<Edge>();

                foreach (var edge in edges)
                {
                    if (edge.Vertex1.Id != maxDegreeVertexId && edge.Vertex2.Id != maxDegreeVertexId)
                    {
                        filteredEdges.Add(edge);
                    }
                }

                edges = filteredEdges;
            }

            return cover;
        }

        public List<uint> SolveApprox(Graph graph, bool useRandom = false)
        {
            List<uint> cover = new List<uint>();

            var edges = graph.Edges.ToList();

            var random = new Random();

            while (edges.Count > 0)
            {
                int index = useRandom ? random.Next(edges.Count) : 0;

                uint vertexId1 = edges[index].Vertex1.Id;
                uint vertexId2 = edges[index].Vertex2.Id;

                cover.Add(vertexId1);
                cover.Add(vertexId2);

                var filteredEdges = new List<Edge>();

                foreach (var edge in edges)
                {
                    if (edge.Vertex1.Id != vertexId1 && edge.Vertex2.Id != vertexId1
                        && edge.Vertex1.Id != vertexId2 && edge.Vertex2.Id != vertexId2)
                    {
                        filteredEdges.Add(edge);
                    }
                }

                edges = filteredEdges;
            }

            return cover.Distinct().ToList();
        }

        public List<uint> SolveBacktracking(Graph graph)
        {
            var edges = graph.Edges.ToList();
            List<uint> cover = Backtrack(edges);
            return cover;
        }

        private List<uint> Backtrack(List<Edge> edges)
        {
            if (edges.Count == 0)
            {
                return new List<uint>();
            }

            uint vertexId1 = edges[0].Vertex1.Id;
            uint vertexId2 = edges[0].Vertex2.Id;

            var edgesWithoutVertex1 = edges.Where(edge => edge.Vertex1.Id != vertexId1 && edge.Vertex2.Id != vertexId1).ToList();
            var coverWithVertex1 = Backtrack(edgesWithoutVertex1);
            coverWithVertex1.Add(vertexId1);

            var edgesWithoutVertex2 = edges.Where(edge => edge.Vertex1.Id != vertexId2 && edge.Vertex2.Id != vertexId2).ToList();
            var coverWithVertex2 = Backtrack(edgesWithoutVertex2);
            coverWithVertex2.Add(vertexId2);

            return coverWithVertex1.Count < coverWithVertex2.Count ? coverWithVertex1 : coverWithVertex2;
        }
    }
}