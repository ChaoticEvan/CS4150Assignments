using System;
using System.Collections.Generic;
using System.Text;

namespace PS5_6
{
    class Program
    {
        static Dictionary<string, Student> graph;
        static void Main(string[] args)
        {
            string currLine = "";
            graph = new Dictionary<string, Student>();
            HashSet<StringBuilder> results = new HashSet<StringBuilder>();

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

            // Do BFS sourcing from ever rumor
            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numRumors);
            for (int k = 0; k < numRumors; k++)
            {
                currLine = Console.ReadLine();
                Dictionary<string, int> dist = new Dictionary<string, int>();
                SortedDictionary<int, SortedSet<string>> test = new SortedDictionary<int, SortedSet<string>>();
                SortedSet<string> finalSet = new SortedSet<string>();

                // Reset graph from previous searches
                foreach (string str in graph.Keys)
                {
                    dist[str] = Int32.MaxValue;
                    finalSet.Add(str);
                }

                dist[currLine] = 0;
                finalSet.Remove(currLine);

                Queue<string> q = new Queue<string>();
                q.Enqueue(currLine);

                while (q.Count != 0)
                {
                    string currStudent = q.Dequeue();
                    foreach (string friend in graph[currStudent].friends)
                    {
                        if (dist[friend] != Int32.MaxValue)
                        {
                            continue;
                        }

                        finalSet.Remove(friend);

                        q.Enqueue(friend);
                        dist[friend] = dist[currStudent] + 1;

                        if (!test.ContainsKey(dist[friend]))
                        {
                            test.Add(dist[friend], new SortedSet<string>());
                        }

                        test[dist[friend]].Add(friend);
                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(currLine);
                foreach (int distance in test.Keys)
                {
                    foreach (string student in test[distance])
                    {
                        sb.Append(" " + student);
                    }
                }
                foreach (string s in finalSet)
                {
                    sb.Append(" " + s);

                }
                results.Add(sb);
            }

            foreach (StringBuilder sb in results)
            {
                Console.WriteLine(sb.ToString());
            }
        }
    }

    class Student
    {
        public HashSet<string> friends { get; set; }
        public string name { get; set; }
        public Student(string name)
        {
            friends = new HashSet<string>();
            this.name = name;
        }
    }
}
