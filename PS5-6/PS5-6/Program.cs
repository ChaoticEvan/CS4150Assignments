using System;
using System.Collections.Generic;

namespace PS5_6
{
    class Program
    {
        static Dictionary<string, Student> graph;
        static void Main(string[] args)
        {
            string currLine = "";
            graph = new Dictionary<string, Student>();

            // Add every student to our graph
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numStudents);
            for (int i = 0; i < numStudents; i++)
            {
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(' ');

                graph.Add(currLineTokens[0], new Student(currLineTokens[0]));
            }

            // Build out the edges given in the second group 
            // of info
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numFriendPairs);
            for (int j = 0; j < numFriendPairs; j++)
            {
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(' ');
                graph[currLineTokens[0]].friends.Add(currLineTokens[1]);
                graph[currLineTokens[1]].friends.Add(currLineTokens[0]);
            }

            // debugging line
        }
    }

    class Student
    {
        public HashSet<string> friends { get; set; }
        public string name { get; set; }
        public bool visited { get; set; }

        public Student(string name)
        {
            friends = new HashSet<string>();
            this.name = name;
        }
    }
}
