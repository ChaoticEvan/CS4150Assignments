using System;
using System.Collections.Generic;

namespace PS6_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string currLine = "";
            currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");
            Dictionary<int, Intersection> graph = new Dictionary<int, Intersection>();

            Int32.TryParse(currLineTokens[0], out int numIntersections);
            Int32.TryParse(currLineTokens[1], out int numCorridors);

            //build graph
            for (int i = 0; i < numCorridors; i++)
            {
                currLine = Console.ReadLine();
                currLineTokens = currLine.Split(" ");

                int x = Int32.Parse(currLineTokens[0]);
                int y = Int32.Parse(currLineTokens[1]);
                float f = float.Parse(currLineTokens[2]);




                if(!graph.ContainsKey(x))
                {
                    Intersection leftIntersection = new Intersection(x);

                    leftIntersection.corridors.Add(y, f);

                    graph.Add(x, leftIntersection);
                }
                else
                {
                    graph[x].corridors.Add(y, f);
                }

                if(!graph.ContainsKey(y))
                {
                    Intersection rightIntersection = new Intersection(y);

                    rightIntersection.corridors.Add(x, f);

                    graph.Add(y, rightIntersection);
                }
                else
                {
                    graph[y].corridors.Add(x, f);
                }
            }


        }
    }

    class Intersection
    {
        public int num { get; set; }
        public Dictionary<int, float> corridors;

        public Intersection(int num)
        {
            this.num = num;
            corridors = new Dictionary<int, float>();
        }
    }
}
