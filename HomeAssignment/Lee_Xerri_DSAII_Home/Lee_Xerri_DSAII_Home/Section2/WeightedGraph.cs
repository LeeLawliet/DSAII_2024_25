using Lee_Xerri_DSAII_Home.Section1;
using Lee_Xerri_DSAII_Home.Section3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lee_Xerri_DSAII_Home.Section2
{
    class WeightedGraph<V> where V : IEquatable<V>
    {
        private ChainingHashTable<V, List<(V, int)>> adjacencyLists = new ChainingHashTable<V, List<(V, int)>>();

        public int Degree(V vertex)
        {
            if (!adjacencyLists.GetAll().Contains(vertex))
                throw new ArgumentException($"The vertex {vertex} does not exist!", nameof(vertex));

            return adjacencyLists.Get(vertex).Count;
        }

        public void AddVertex(V vertex)
        {
            if (adjacencyLists.GetAll().Contains(vertex))
                throw new Exception($"Vertex {vertex} already exists!");

            adjacencyLists.AddOrUpdate(vertex, new List<(V, int)>());
        }

        public void AddEdge(V vertexOne, V vertexTwo, int weight)
        {
            if (!adjacencyLists.GetAll().Contains(vertexOne))
                AddVertex(vertexOne);

            if (!adjacencyLists.GetAll().Contains(vertexTwo))
                AddVertex(vertexTwo);

            var listOne = adjacencyLists.Get(vertexOne);
            var listTwo = adjacencyLists.Get(vertexTwo);

            if (listOne.Contains((vertexTwo, weight)) || listTwo.Contains((vertexOne, weight)))
                throw new Exception("Edge already exists!");

            listOne.Add((vertexTwo, weight));
            listTwo.Add((vertexOne, weight));
        }

        public bool IsAdjacent(V vertexOne, V vertexTwo)
        {
            if (!adjacencyLists.GetAll().Contains(vertexOne))
                throw new ArgumentException($"Vertex {vertexOne} does not exist!", nameof(vertexOne));

            if (!adjacencyLists.GetAll().Contains(vertexTwo))
                throw new ArgumentException($"Vertex {vertexTwo} does not exist!", nameof(vertexTwo));

            foreach (var (adjVertex, _) in adjacencyLists.Get(vertexOne))
            {
                if (adjVertex.Equals(vertexTwo))
                    return true;
            }

            return false;
        }

        public IEnumerable<(V, int)> Adjacencies(V vertex)
        {
            if (!adjacencyLists.GetAll().Contains(vertex))
                throw new ArgumentException($"Vertex {vertex} does not exist!", nameof(vertex));

            return adjacencyLists.Get(vertex);
        }

        public IEnumerable<V> GetAllVertices()
        {
            return adjacencyLists.GetAll();
        }

        public GraphSearchResult<V> DijkstraABV(WeightedGraph<V> graph, V source)
        {
            var priorityQueue = new ArrayBasedVectorPriorityQueue<V>();
            var result = new GraphSearchResult<V>(source);

            foreach (V vertex in graph.GetAllVertices())
            {
                if (vertex.Equals(source)) continue;

                result.DistanceToSource[vertex] = int.MaxValue;
                priorityQueue.Add(vertex);
            }

            priorityQueue.Add(source, 0);
            result.DistanceToSource[source] = 0;

            while (!priorityQueue.IsEmpty())
            {
                V u = priorityQueue.RemoveMin();

                foreach (var (adjacentVertex, weight) in graph.Adjacencies(u))
                {
                    V v = adjacentVertex;
                    int newDistance = result.DistanceToSource[u] + weight;

                    if (newDistance < result.DistanceToSource[v])
                    {
                        result.DistanceToSource[v] = newDistance;
                        priorityQueue.DecreaseKey(v, newDistance);
                        result.Previous[v] = u;
                    }
                }
            }

            return result;
        }

        public Dictionary<V, int> DijkstraCHT(V source)
        {
            var distances = new Dictionary<V, int>();
            var previous = new Dictionary<V, V>();
            var processed = new HashSet<V>();

            var priorityQueue = new ChainingHashTable<V, int>();

            foreach (var vertex in adjacencyLists.GetAll())
            {
                if (vertex.Equals(source))
                {
                    distances[vertex] = 0;
                    priorityQueue.AddOrUpdate(vertex, 0);
                }
                else
                {
                    distances[vertex] = int.MaxValue;
                    priorityQueue.AddOrUpdate(vertex, int.MaxValue);
                }
                previous[vertex] = default(V);
            }

            while (priorityQueue.Size > 0)
            {
                V currentVertex = GetMinVertex(priorityQueue);

                if (distances[currentVertex] == int.MaxValue) break;

                processed.Add(currentVertex);
                priorityQueue.Remove(currentVertex);

                foreach (var (neighbor, weight) in Adjacencies(currentVertex))
                {
                    int newDistance = distances[currentVertex] + weight;

                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                        previous[neighbor] = currentVertex;

                        priorityQueue.AddOrUpdate(neighbor, newDistance);
                    }
                }
            }

            return distances;
        }

        private V GetMinVertex(ChainingHashTable<V, int> priorityQueue)
        {
            V minVertex = default(V);
            int minDistance = int.MaxValue;

            foreach (var vertex in priorityQueue.GetAll())
            {
                int distance = priorityQueue.Get(vertex);
                if (distance < minDistance)
                {
                    minVertex = vertex;
                    minDistance = distance;
                }
            }

            return minVertex;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("graph G {");

            foreach (var vertex in adjacencyLists.GetAll())
            {
                sb.AppendLine($"{vertex.ToString()};");
            }

            var addedEdges = new HashSet<string>();

            foreach (var vertex in adjacencyLists.GetAll())
            {
                var adjacencyList = adjacencyLists.Get(vertex);

                foreach (var (adjacentVertex, weight) in adjacencyList)
                {
                    string edge = $"{vertex.ToString()}--{adjacentVertex.ToString()} [label=\"{weight}\"];";
                    string reverseEdge = $"{adjacentVertex.ToString()}--{vertex.ToString()} [label=\"{weight}\"];";

                    if (addedEdges.Contains(edge)) continue;

                    sb.AppendLine(edge);
                    addedEdges.Add(edge);
                    addedEdges.Add(reverseEdge);
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
