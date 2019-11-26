using System;
using System.Collections.Generic;
using System.Text;

namespace PS10_4
{
    class Program
    {
        private static Node[,] gallery;
        private static SortedDictionary<int, HashSet<Node>> values;
        private static Dictionary<string, int> cache;

        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");
            cache = new Dictionary<string, int>();
            Int32.TryParse(currLineTokens[0], out int numOfRows);
            Int32.TryParse(currLineTokens[1], out int numRoomsToClose);
            BuildGallery(numOfRows);
            // TO DO
            // USE DYNAMIC PROGRAMMING ALGO
            Console.WriteLine(MaxValues(0, -1, numRoomsToClose));
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

            string cacheKey = r + " " + unclosableRoom + " " + numRooms;
            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }
            int result = -99;
            if (numRooms == (gallery.Length / 2) - r)
            {
                switch (unclosableRoom)
                {
                    case -1:
                        result = Math.Max(gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1), gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1));
                        break;
                    case 0:
                        result = gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1);
                        break;
                    case 1:
                        result = gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1);
                        break;
                }
            }
            else if (numRooms < (gallery.Length / 2) - r)
            {
                switch (unclosableRoom)
                {
                    case -1:
                        result = Math.Max(gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1), Math.Max(gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms), gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1)));
                        break;
                    case 0:
                        result = Math.Max(gallery[r, 0].value + MaxValues(r + 1, 0, numRooms - 1), gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms));
                        break;
                    case 1:
                        result = Math.Max(gallery[r, 1].value + MaxValues(r + 1, 1, numRooms - 1), gallery[r, 0].value + gallery[r, 1].value + MaxValues(r + 1, -1, numRooms));
                        break;
                }
            }
            cache.Add(cacheKey, result);
            return result;
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
                sb.AppendLine(gallery[i, 0].isClosed + " " + gallery[i, 1].isClosed);
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
