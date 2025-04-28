using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revision
{
    public class Graph
    {
        List<Vertex> vertices;

        public Graph(string data)
        {
            string[] sLines = data.Split('\n');

            bool? workingOnVertices = null;

            foreach (string line in sLines)
            {
                if (line == "Vertices")
                {
                    workingOnVertices = true;
                    continue;
                }
                else if(line == "Adjacencies")
                {
                    workingOnVertices = false;
                    continue;
                }
                else
                {
                    if (workingOnVertices == null)
                    {
                        throw new Exception("Parse Issue");
                    }
                    else if (workingOnVertices == true)
                    {
                        string[] vNames = line.Split(',', (StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
                        foreach (string vName in vNames)
                        {
                            Vertex newV = new Vertex(vName);
                            this.AddVertex(newV);
                        }
                    }
                    else if (workingOnVertices == false)
                    {
                        string[] edgeInfo = line.Split(',', (StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
                        
                        if (edgeInfo.Length != 3)
                        {
                            // Parse issue
                            throw new Exception("Parse Issue");
                        }
                        else
                        {
                            this.AddAdjacency(new Vertex(edgeInfo[0]), new Vertex(edgeInfo[1]), Convert.ToInt32(edgeInfo[2]));
                        }
                    }
                }
            }
        }

        private Vertex GetVertex(string label)
        {
            if (vertices == null)
            {
                vertices = new List<Vertex>();
            }

            foreach (Vertex v in vertices)
            {
                if (v.Label == label)
                {
                    return v;
                }
            }

            throw new Exception("Vertex not found");
        }

        public void AddVertex(Vertex v)
        {
            if (v == null)
            {
                throw new Exception("Vertex provided is null.");
            }

            if (this.vertices.Contains(v))
            {
                throw new Exception("Vertex already exists.");
            }
            else
            {
                this.vertices.Add(v);
            }
        }

        public void AddAdjacency(Vertex vAdj1, Vertex vAdj2, int weight)
        {
            if (vAdj1 == null || vAdj2 == null)
            {
                throw new NullReferenceException("Vertices provided is null");
            }
            else
            {
                if (vAdj1.adjacencies.ContainsKey(vAdj2) || vAdj2.adjacencies.ContainsKey(vAdj1))
                {
                    throw new Exception("Vertex provided already exists.");
                }
                else
                {
                    vAdj1.adjacencies.Add(vAdj2, weight);
                    vAdj2.adjacencies.Add(vAdj1, weight);
                }
            }
        }

        public Dictionary<Vertex, int> GetAdjacencies(Vertex v)
        {
            if (v == null)
            {
                throw new Exception("Vertex is null");
            }
            else
            {
                return v.adjacencies;
            }
        }
    }
}
