using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Revision
{
    public class Vertex
    {
        public string Label { get; set; }
        public Dictionary<Vertex, int> adjacencies;
        public int Weight {get; set;}

        public Vertex(string label)
        {
            this.Label = label;
            this.adjacencies = new Dictionary<Vertex, int>();
        }

        public void AddAdjacency(Vertex vAdj, int weight)
        {
            if (vAdj == null)
            {
                throw new NullReferenceException("Vertex provided is null");
            }
            else
            {
                if (this.adjacencies.ContainsKey(vAdj))
                {
                    throw new Exception("Vertex provided already exists.");
                }
                else
                {
                    this.adjacencies.Add(vAdj, weight);
                    vAdj.adjacencies.Add(this, weight);
                }
            }
        }

        // Override Equals for vertex comparison based on label
        public override bool Equals(object obj)
        {
            return obj is Vertex vertex && Label == vertex.Label;
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }

        public int IsAdjacency(Vertex adj)
        {
            if (adj == null)
            {
                Console.WriteLine("Vertex provided is null.");
            }
            else
            {
                if (this.adjacencies.ContainsKey(adj))
                {
                    return adj.Weight;
                }
            }
            return 0;
        }
    }
}
