using System;
using System.Text;

namespace PS10_4
{
    class Program
    {
        private static Node[,] gallery;
        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");

            Int32.TryParse(currLineTokens[0], out int numOfRows);
            BuildGallery(numOfRows);

            // TO DO
            // BUILD ALGORITHM TO SOLVE

            Console.WriteLine(SumOpenRooms());
        }

        private static void BuildGallery(int numOfRows)
        {
            string currLine = "";
            string[] currLineTokens;
            gallery = new Node[numOfRows, 2];

            for (int i = 0; i < numOfRows; ++i)
            {
                // Parse the current line and add the nodes to our 2D array
                currLine = Console.ReadLine();
                currLineTokens = currLine.Split(" ");
                gallery[i, 0] = new Node()
                {
                    value = Int32.Parse(currLineTokens[0]),
                    canClose = true,
                    isClosed = false
                };
                gallery[i, 1] = new Node()
                {
                    value = Int32.Parse(currLineTokens[1]),
                    canClose = true,
                    isClosed = false
                };
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
            for (int i = 0; i < (gallery.Length/2); ++i)
            {
                sb.AppendLine(gallery[i, 0].value + " " + gallery[i, 1].value);
            }

            Console.WriteLine(sb.ToString());
        }

        private static int SumOpenRooms()
        {
            int sum = 0;
            // Divide the length by 2 because we have 2 columns, and
            // the length is the number of elements in all columns and
            // all rows.
            for (int i = 0; i < (gallery.Length / 2); ++i)
            {
                if(!gallery[i, 0].isClosed)
                {
                    sum += gallery[i, 0].value;
                }
                if(!gallery[i, 1].isClosed)
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
        public bool canClose { get; set; }
        public bool isClosed { get; set; }

    }
}
