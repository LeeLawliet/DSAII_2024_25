using Lee_Xerri_DSAII_Home.Section1;
using Lee_Xerri_DSAII_Home.Section2;
using Lee_Xerri_DSAII_Home.Section3;
using Lee_Xerri_DSAII_Home.Section4;
using System;
using System.Collections.Generic;

namespace Lee_Xerri_DSAII_Home
{
    public class Program
    {
        public static string PrintSection(string sectionName)
        {
            return ("\n---------------------------------------------------\n" + sectionName + "\n");
        }

        static int GetEdgeWeight(WeightedGraph<string> graph, string vertexOne, string vertexTwo)
        {
            foreach (var (adjacent, weight) in graph.Adjacencies(vertexOne))
            {
                if (adjacent.Equals(vertexTwo))
                {
                    return weight;
                }
            }
            return 0;
        }

        static void Main()
        {
            // SECTION 1 - TASK 2s
            Console.WriteLine(PrintSection("SECTION 1 - TASK 2"));

            var hashTable = new ChainingHashTable<int, string>();
            var random = new Random();
            var originalData = new Dictionary<int, string>();

            while (originalData.Count < 100)
            {
                int key = random.Next(1, 1000);
                if (!originalData.ContainsKey(key))
                {
                    originalData[key] = "Value_" + key;
                }
            }

            Console.WriteLine("Adding 100 key-value pairs...");
            foreach (var kvp in originalData)
                hashTable.AddOrUpdate(kvp.Key, kvp.Value);

            var retrievedKeys = hashTable.GetAll();
            Console.WriteLine($"Total keys retrieved: {retrievedKeys.Count}");

            if (retrievedKeys.OrderBy(k => k).SequenceEqual(originalData.Keys.OrderBy(k => k)))
                Console.WriteLine("All keys successfully retrieved!");

            bool allValuesMatch = true;
            foreach (var key in retrievedKeys)
            {
                if (hashTable.Get(key) != originalData[key])
                {
                    allValuesMatch = false;
                    break;
                }
            }

            Console.WriteLine(allValuesMatch ? "All values match!" : "Value mismatch found.");

            // SECTION 2 - TASK 2 PT.1 (ARRAY BASED VECTOR)

            Console.WriteLine(PrintSection("SECTION 2 - TASK 2 PT.1 (ARRAY BASED VECTOR)"));

            WeightedGraph<string> graph = new WeightedGraph<string>();

            // Add vertices and weighted edges
            graph.AddEdge("A", "B", 4);
            graph.AddEdge("A", "C", 1);
            graph.AddEdge("B", "C", 2);
            graph.AddEdge("B", "D", 5);
            graph.AddEdge("C", "D", 8);
            graph.AddEdge("C", "E", 10);
            graph.AddEdge("D", "E", 2);

            // Run Dijkstra's algorithm from every vertex
            Console.WriteLine("Shortest distances from each vertex (ArrayBasedVector):");

            foreach (var vertex in graph.GetAllVertices())
            {
                // Call Dijkstra for each vertex in the graph
                var result = graph.DijkstraABV(graph, vertex);

                // Print the shortest distances from each vertex
                Console.WriteLine($"\nFrom vertex {vertex}:");

                // Access and print the distances from the source vertex to all other vertices
                foreach (var distance in result.DistanceToSource)
                {
                    Console.WriteLine($"{distance.Key}: {distance.Value}");
                }
            }

            // SECTION 2 - TASK 2 PT.2 (CHAINING HASH TABLE)

            Console.WriteLine(PrintSection("SECTION 2 - TASK 2 PT.2 (CHAINING HASH TABLE)"));

            // Run Dijkstra's algorithm from every vertex
            Console.WriteLine("Shortest distances from each vertex (Chaining Hash table):");

            foreach (var vertex in graph.GetAllVertices())
            {
                var chtDistances = graph.DijkstraCHT(vertex);

                // Print the shortest distances from each vertex
                Console.WriteLine($"\nFrom vertex {vertex}:");
                foreach (var distance in chtDistances)
                {
                    Console.WriteLine($"{distance.Key}: {distance.Value}");
                }
            }

            // SECTION 2 - TASK 3

            Console.WriteLine(PrintSection("SECTION 2 - TASK 3"));

            graph = new WeightedGraph<string>();

            graph.AddEdge("A", "B", 8);
            graph.AddEdge("A", "C", 12);
            graph.AddEdge("B", "C", 13);
            graph.AddEdge("B", "D", 25);
            graph.AddEdge("C", "D", 14);
            graph.AddEdge("B", "E", 9);
            graph.AddEdge("D", "E", 20);
            graph.AddEdge("D", "F", 8);
            graph.AddEdge("E", "F", 19);
            graph.AddEdge("C", "G", 21);
            graph.AddEdge("D", "G", 12);
            graph.AddEdge("D", "H", 12);
            graph.AddEdge("F", "H", 11);
            graph.AddEdge("D", "I", 16);
            graph.AddEdge("G", "I", 11);
            graph.AddEdge("H", "I", 9);

            var newResult = graph.DijkstraABV(graph, "A");
            var distances = newResult.DistanceToSource;
            var previous = newResult.Previous;

            Console.WriteLine("\nPath from A to H:");

            List<string> path = new List<string>();
            int totalWeight = 0;
            string current = "H";

            // Trace the path from H to A
            while (current != "A")
            {
                path.Insert(0, current); // Insert at the beginning of the list to reverse the order
                string previousVertex = previous.ContainsKey(current) ? previous[current] : null;

                if (previousVertex == null)
                {
                    Console.WriteLine("No path from A to H.");
                    return;
                }

                totalWeight += GetEdgeWeight(graph, current, previousVertex);

                current = previousVertex;
            }

            // Add the starting vertex A to the path
            path.Insert(0, "A");

            // Print the path and total weight
            Console.WriteLine("Path: " + string.Join(" -> ", path));
            Console.WriteLine($"Total weight: {totalWeight}");

            // SECTION 4 - TASK 2

            Console.WriteLine(PrintSection("SECTION 2 - TASK 3"));

            List<int> numbers = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                int rand = random.Next(1, 1000);
                numbers.Add(rand);
            }

            List<int> numbersCopy = new List<int>(numbers);

            List<int> sortedNumbersMS = MergeSortTask.MergeSort(numbersCopy);
            List<int> builtinSort = new List<int>(numbersCopy);
            builtinSort.Sort();

            int match = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (sortedNumbersMS[i] != builtinSort[i])
                {
                    Console.WriteLine($"Number at index {i} are not the same: MergeSort Value -> {sortedNumbersMS[i]} | Built-in -> {builtinSort[i]}");
                }

                if (sortedNumbersMS[i] == builtinSort[i])
                {
                    match++;
                }
            }

            if (match != 1000)
            {
                Console.WriteLine($"Total matches were: {match}. The arrays are mis-matched.");
            }
        }
    }
}
