using System;
using System.Collections.Generic;

namespace PS4_5
{
    class Program
    {
        Dictionary<string, Vertex> graph;
        static void Main(string[] args)
        {
            
        }
    }

    class Vertex
    {
        HashSet<Edge> leavingEdges { get; set; }
        HashSet<Edge> incomingEdges { get; set; }

        string name;

        public Vertex(string name)
        {
            this.name = name;
        }
    }

    class Edge
    {
        int weight;
        Vertex from;
        Vertex to;

        Edge(int weight, Vertex from, Vertex to)
        {
            this.weight = weight;
            this.from = from;
            this.to = to;
        }
    }
}
