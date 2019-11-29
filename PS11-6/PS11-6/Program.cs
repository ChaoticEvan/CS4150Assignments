using System;
using System.Collections.Generic;

namespace PS11_6
{
    class Program
    {

        /// <summary>
        /// Hashset of exercises from input
        /// </summary>
        public static HashSet<int[]> exercises;

        /// <summary>
        /// List of solutions
        /// </summary>
        public static List<string> solutions;

        /// <summary>
        /// Main method used for solving the Spiderman's Workout Kattis problem.
        /// </summary>
        /// <param name="args">UNUSED</param>
        static void Main(string[] args)
        {
            string currLine = "";
            exercises = new HashSet<int[]>();
            solutions = new List<string>();

            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numExercises);

            // Reads all input
            BuildExercises(numExercises);

            foreach (int[] list in exercises)
            {
                solutions.Add(CalcSolution(list));
            }

            foreach (string s in solutions)
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Build dictionary from input. Input should contain 2 * parameter lines
        /// </summary>
        /// <param name="numExercises">Number of exercises to read in from input</param>
        public static void BuildExercises(int numExercises)
        {
            string currLine = "";
            for (int i = 0; i < numExercises; ++i)
            {
                // Read number of steps
                currLine = Console.ReadLine();
                Int32.TryParse(currLine, out int numSteps);

                // Read the individual steps and add them to our list
                currLine = Console.ReadLine();
                string[] currLineTokens = currLine.Split(" ");
                int[] temp = new int[numSteps];
                for (int j = 0; j < numSteps; ++j)
                {
                    temp[j] = Int32.Parse(currLineTokens[j]);
                }

                // Add our entry into our dictionary to do the work on later.
                // The length of temp is going to be our number of steps for 
                // future reference.
                exercises.Add(temp);
            }
        }

        public static string CalcSolution(int[] list)
        {
            // Build out necessary variables
            int max = 0;
            int largest = 0;
            foreach (int i in list)
            {
                max += i;
                if (i > largest)
                {
                    largest = i;
                }
            }

            largest += max + 1;

            // These two 2D arrays act as our cache.
            // One for keeping track of the numbers, and
            // one for keeping track of the path we took.
            int[,] pos = new int[list.Length + 1, largest];
            string[,] directions = new string[list.Length + 1, largest];

            // Fill our number cache with our calculated max,
            // so we only replace it with possible better values
            for (int i = 0; i < list.Length + 1; ++i)
            {
                for (int j = 0; j < largest; ++j)
                {
                    pos[i, j] = max;
                }
            }

            // Our initial best height is 0
            pos[0, 0] = 0;

            // Iterative solution, as opposed to recursive solution.
            // Really struggled with the logic for the recursion, but 
            // this iteration made much more sense to me.
            for (int i = 0; i < list.Length; ++i)
            {
                for (int j = 0; j < max; ++j)
                {
                    int currHeight = pos[i, j];
                    int upTemp = j + list[i];
                    int downTemp = j - list[i];

                    // If going up is viable and is our better solution
                    // cache it
                    if (pos[i + 1, upTemp] > Math.Max(upTemp, currHeight))
                    {
                        pos[i + 1, upTemp] = Math.Max(upTemp, currHeight);
                        directions[i + 1, upTemp] = "U";
                    }


                    // If going down is viable and is our better solution
                    // cache it
                    if (downTemp >= 0 && pos[i + 1, downTemp] > currHeight)
                    {
                        pos[i + 1, downTemp] = currHeight;
                        directions[i + 1, downTemp] = "D";
                    }
                }
            }

            string currResult = "";
            int height = pos[list.Length, 0];
            // If there is a solution...
            if (height != max)
            {
                int dist = 0;

                // Go through our list and find the 
                // path we took to obtain a solution
                for (int i = list.Length; i > 0; --i)
                {
                    if (directions[i, dist].Equals("U"))
                    {
                        dist -= list[i - 1];
                        currResult += "U";
                    }
                    else
                    {
                        dist += list[i - 1];
                        currResult += "D";
                    }
                }

                // Reverse the our path, because we had to go through our list backwards.
                // This method of reversing a string was recommended at https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
                char[] revSol = currResult.ToCharArray();
                Array.Reverse(revSol);
                return new string(revSol);
            }
            // If our cached height is the max then we know there is no solution
            else
            {
                return "IMPOSSIBLE";
            }
        }
    }
}
