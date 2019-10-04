using System;
using System.Collections.Generic;

namespace PS6_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string currLine = "";
            HashSet<float> results = new HashSet<float>();
            while (!currLine.Equals("0 0"))
            {
                currLine = Console.ReadLine();
                if(currLine.Equals("0 0"))
                {
                    break;
                }
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




                    if (!graph.ContainsKey(x))
                    {
                        Intersection leftIntersection = new Intersection(x);

                        leftIntersection.corridors.Add(y, f);

                        graph.Add(x, leftIntersection);
                    }
                    else
                    {
                        graph[x].corridors.Add(y, f);
                    }

                    if (!graph.ContainsKey(y))
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

                Dictionary<int, float> dist = new Dictionary<int, float>();


                for(int i = 0; i < numIntersections; i++)
                {
                    dist.Add(i, float.MaxValue);
                }
                dist[0] = 0;

                PriorityQueue pq = new PriorityQueue();
                Tuple<int, float> tup = Tuple.Create(0, 0f);
                pq.Enqueue(tup);

                while(pq.Count() != 0)
                {
                    Tuple<int, float> curr = pq.Dequeue();
                    foreach (int neighbor in graph[curr.Item1].corridors.Keys)
                    {
                        if(dist[neighbor] > dist[curr.Item1] + graph[curr.Item1].corridors[neighbor])
                        {
                            dist[neighbor] = dist[curr.Item1] + graph[curr.Item1].corridors[neighbor];

                            Tuple<int, float> currTup = Tuple.Create(neighbor, dist[neighbor]);
                            pq.Enqueue(currTup);
                        }
                    }
                }

                results.Add(dist[numCorridors - 1]);
            }

            foreach(float f in results)
            {
                Console.WriteLine(f);
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

    /// <summary>
    /// I grabbed this PQ from the page linked below.
    /// URL: https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue
    {
        private List<Tuple<int, float>> data;

        public PriorityQueue()
        {
            this.data = new List<Tuple<int, float>>();
        }

        public void Enqueue(Tuple<int, float> item)
        {
            data.Add(item);
            int ci = data.Count - 1; // child index; start at end
            while (ci > 0)
            {
                int pi = (ci - 1) / 2; // parent index                                      
                if (data[ci].Item2.CompareTo(data[pi].Item2) >= 0) break; // child item is larger than (or equal) parent so we're done
                Tuple<int, float> tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                ci = pi;
            }
        }

        public Tuple<int, float> Dequeue()
        {
            // assumes pq is not empty; up to calling code
            int li = data.Count - 1; // last index (before removal)
            Tuple<int, float> frontItem = data[0];   // fetch the front
            data[0] = data[li];
            data.RemoveAt(li);

            --li; // last index (after removal)
            int pi = 0; // parent index. start at front of pq
            while (true)
            {
                int ci = pi * 2 + 1; // left child index of parent
                if (ci > li) break;  // no children so done
                int rc = ci + 1;     // right child
                if (rc <= li && data[rc].Item2.CompareTo(data[ci].Item2) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                    ci = rc;
                if (data[pi].Item2.CompareTo(data[ci].Item2) <= 0) break; // parent is smaller than (or equal to) smallest child so done
                Tuple<int, float> tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
                pi = ci;
            }
            return frontItem;
        }

        public Tuple<int, float> Peek()
        {
            Tuple<int, float> frontItem = data[0];
            return frontItem;
        }

        public int Count()
        {
            return data.Count;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < data.Count; ++i)
                s += data[i].ToString() + " ";
            s += "count = " + data.Count;
            return s;
        }

        public bool IsConsistent()
        {
            // is the heap property true for all data?
            if (data.Count == 0) return true;
            int li = data.Count - 1; // last index
            for (int pi = 0; pi < data.Count; ++pi) // each parent index
            {
                int lci = 2 * pi + 1; // left child index
                int rci = 2 * pi + 2; // right child index

                if (lci <= li && data[pi].Item2.CompareTo(data[lci].Item2) > 0) return false; // if lc exists and it's greater than parent then bad.
                if (rci <= li && data[pi].Item2.CompareTo(data[rci].Item2) > 0) return false; // check the right child too.
            }
            return true; // passed all checks
        } // IsConsistent
    } // PriorityQueue

} // ns

