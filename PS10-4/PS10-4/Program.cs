using System;
using System.Collections.Generic;
using System.Text;

namespace PS10_4
{
    class Program
    {
        private static Node[,] gallery;
        private static SortedDictionary<int, HashSet<Node>> values;

        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");

            Int32.TryParse(currLineTokens[0], out int numOfRows);
            Int32.TryParse(currLineTokens[1], out int numRoomsToClose);
            BuildGallery(numOfRows);

            // TO DO
            // DEBUG OPEN/CLOSED ROOMS
            int currRoomsClosed = 0;
            foreach (int key in values.Keys)
            {
                foreach (Node node in values[key])
                {
                    if(currRoomsClosed == numRoomsToClose)
                    {
                        break;
                    }
                    Node currNode = gallery[node.row, node.col];

                    if (currNode.canClose && !currNode.isClosed)
                    {
                        currNode.isClosed = true;
                        CloseNeighbors(currNode);
                        ++currRoomsClosed;
                    }
                }
            }

            Console.WriteLine(SumOpenRooms());
        }

        private static void CloseNeighbors(Node currNode)
        {
            // Can't close neighbor in same row
            if (currNode.col == 1)
            {
                gallery[currNode.row, 0].canClose = false;
            }
            else
            {
                gallery[currNode.row, 1].canClose = false;
            }

            // Can't close diagonal above
            if (currNode.row > 0 && currNode.col == 0)
            {
                gallery[currNode.row - 1, 1].canClose = false;
            }
            else if (currNode.row > 0 && currNode.col == 1)
            {
                gallery[currNode.row - 1, 0].canClose = false;
            }

            // Can't close diagonal below
            if (currNode.row < (gallery.Length / 2) - 1 && currNode.col == 0)
            {
                gallery[currNode.row + 1, 1].canClose = false;
            }
            else if (currNode.row < (gallery.Length / 2) - 1 && currNode.col == 1)
            {
                gallery[currNode.row + 1, 0].canClose = false;
            }
        }

        private static void BuildGallery(int numOfRows)
        {
            string currLine = "";
            string[] currLineTokens;
            gallery = new Node[numOfRows, 2];
            values = new SortedDictionary<int, HashSet<Node>>(new DescendingComparer<int>());

            for (int i = 0; i < numOfRows; ++i)
            {
                // Parse the current line and add the nodes to our 2D array
                currLine = Console.ReadLine();
                currLineTokens = currLine.Split(" ");
                int leftVal = Int32.Parse(currLineTokens[0]), rightVal = Int32.Parse(currLineTokens[1]);
                gallery[i, 0] = new Node()
                {
                    value = leftVal,
                    row = i,
                    col = 0,
                    canClose = true,
                    isClosed = false
                };
                gallery[i, 1] = new Node()
                {
                    value = rightVal,
                    row = i,
                    col = 1,
                    canClose = true,
                    isClosed = false
                };

                if (values.ContainsKey(leftVal))
                {
                    values[leftVal].Add(gallery[i, 0]);
                }
                else
                {
                    values[leftVal] = new HashSet<Node>();
                    values[leftVal].Add(gallery[i, 0]);
                }

                if (values.ContainsKey(rightVal))
                {
                    values[rightVal].Add(gallery[i, 1]);
                }
                else
                {
                    values[rightVal] = new HashSet<Node>();
                    values[rightVal].Add(gallery[i, 1]);
                }
            }
        }


        /// <summary>
        /// Method only for testing that the gallery is printing correctly.
        /// </summary>
        private static void PrintGallery()
        {
            StringBuilder sb = new StringBuilder();

            // Divide the length by 2 because we have 2 columns, and
            // the length is the number of elements in all columns and
            // all rows.
            for (int i = 0; i < (gallery.Length / 2); ++i)
            {
                sb.AppendLine(gallery[i, 0].value + " " + gallery[i, 1].value);
            }

            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Sums the all the vlaues of the open rooms
        /// </summary>
        /// <returns>Summed value of every room in gallery that is open</returns>
        private static int SumOpenRooms()
        {
            int sum = 0;
            // Divide the length by 2 because we have 2 columns, and
            // the length is the number of elements in all columns and
            // all rows.
            for (int i = 0; i < (gallery.Length / 2); ++i)
            {
                if (!gallery[i, 0].isClosed)
                {
                    sum += gallery[i, 0].value;
                }
                if (!gallery[i, 1].isClosed)
                {
                    sum += gallery[i, 1].value;
                }
            }
            return sum;
        }
    }

    public struct Node
    {
        public int value { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public bool canClose { get; set; }
        public bool isClosed { get; set; }

    }

    class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x);
        }
    }
}
