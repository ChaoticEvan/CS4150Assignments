using System;

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
    }

    public struct Node
    {
        public int value { get; set; }
        public bool canClose { get; set; }
    }
}
