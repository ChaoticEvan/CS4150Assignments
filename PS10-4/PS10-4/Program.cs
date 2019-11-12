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
            return;
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
                    canClose = true
                };
                gallery[i, 1] = new Node()
                {
                    value = Int32.Parse(currLineTokens[1]),
                    canClose = true
                };
            }
        }

        /// <summary>
        /// Method only for testing that the gallery is printing correctly.
        /// </summary>
        private static void PrintGallery()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < (gallery.Length/2); ++i)
            {
                sb.AppendLine(gallery[i, 0].value + " " + gallery[i, 1].value);
            }

            Console.WriteLine(sb.ToString());
        }
    }

    public struct Node
    {
        public int value { get; set; }
        public bool canClose { get; set; }
    }
}
