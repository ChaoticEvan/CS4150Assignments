using System;
using System.Collections.Generic;

namespace PS4_5
{
    class Program
    {

        static Dictionary<string, Vertex> graph;
        static void Main(string[] args)
        {
            string currLine = "";
            graph = new Dictionary<string, Vertex>();
            List<string> results = new List<string>();

            // Build out the vertexes given in the first
            // group of info
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numVertexes);
            for (int i = 0; i < numVertexes; i++)
            {
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(' ');

                graph.Add(currLineTokens[0], new Vertex(currLineTokens[0], Int32.Parse(currLineTokens[1])));
            }

            // Build out the edges given in the second group 
            // of info
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numEdges);
            for (int j = 0; j < numEdges; j++)
            {
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(' ');

                graph[currLineTokens[0]].leavingEdges.Add(currLineTokens[1]);
                graph[currLineTokens[1]].incomingEdges.Add(currLineTokens[0]);
            }

            // Loop through each trip and print out 
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numTrips);
            for(int k = 0; k < numTrips; k++)
            {                
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(' ');

                string from = currLineTokens[0];
                string to = currLineTokens[1];

                // If origin and destination are the same
                // Then cost is 0
                if(from.Equals(to))
                {
                    results.Add("0");
                    continue;
                }
                // TODO DO DJIKSTRA'S to find cheapest path from from to to
            }


            // Print out all results at the end
            foreach (string s in results)
            {
                Console.WriteLine(s);
            }
        }
    }

    class Vertex
    {
        public HashSet<string> leavingEdges { get; set; }
        public HashSet<string> incomingEdges { get; set; }
        public int cost { get; set; }
        public string name { get; set; }

        public Vertex(string name, int cost)
        {
            leavingEdges = new HashSet<string>();
            incomingEdges = new HashSet<string>();
            this.cost = cost;
            this.name = name;
        }
    }
}
