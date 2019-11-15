using System;
using System.Collections.Generic;
using System.Text;

namespace PS10_4
{
    class Program
    {
        private static Node[,] gallery;
        private static int[,] cache;
        private static SortedDictionary<int, HashSet<Node>> values;

        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");

            Int32.TryParse(currLineTokens[0], out int numOfRows);
            Int32.TryParse(currLineTokens[1], out int numRoomsToClose);
            BuildGallery(numOfRows);
            SetupCache(numOfRows);
            // TO DO
            // USE DYNAMIC PROGRAMMING ALGO
            Console.WriteLine(MaxValues(0, -1, numRoomsToClose));
        }

        /// <summary>
        /// Helper method for closing the proper neighbors
        /// </summary>
        /// <param name="currNode">Node to close</param>
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

        /// <summary>
        /// Helper method for building 2D array
        /// from the input given
        /// </summary>
        /// <param name="numOfRows">Number of rows to read from input</param>
        private static void BuildGallery(int numOfRows)
        {
            string currLine = "";
            string[] currLineTokens;
            gallery = new Node[numOfRows, 2];
            values = new SortedDictionary<int, HashSet<Node>>();

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

        private static int MaxValues(int r, int unclosableRoom, int numRooms)
        {
            if (r >= gallery.Length / 2)
            {
                return 0;
            }

            if (unclosableRoom == 0 && cache[r, 0] != -99)
            {
                return cache[r, 0];
            }
            else if (unclosableRoom == 1 && cache[r, 1] != -99)
            {
                return cache[r, 1];
            }
            else if (cache[r, 0] != -99 && cache[r, 1] != -99)
            {
                return cache[r, 0] + cache[r, 1];
            }
            int result = -99;
            if (numRooms == (gallery.Length / 2) - r)
            {
                switch (unclosableRoom)
                {
                    case -1:
                        result = Math.Max(gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1), gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1));
                        cache[r, 0] = result;
                        break;
                    case 0:
                        result = gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1);
                        cache[r, 0] = result;
                        break;
                    case 1:
                        result = gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1);
                        cache[r, 1] = result;
                        break;
                }
            }
            if (numRooms < (gallery.Length / 2) - r)
            {
                switch (unclosableRoom)
                {
                    case -1:
                        result = Math.Max(gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1), Math.Max(gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms), gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1)));
                        cache[r, 0] = result;
                        break;
                    case 0:
                        result = Math.Max(gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1), gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms));
                        cache[r, 0] = result;
                        break;
                    case 1:
                        result = Math.Max(gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1), gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms));
                        cache[r, 1] = result;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Initialize all values in cache to -99
        /// for null checks
        /// </summary>
        /// <param name="numOfRows">Number of rows from input</param>
        private static void SetupCache(int numOfRows)
        {
            cache = new int[numOfRows, 2];
            for (int i = 0; i < numOfRows; ++i)
            {
                cache[i, 0] = -99;
                cache[i, 1] = -99;
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
}
