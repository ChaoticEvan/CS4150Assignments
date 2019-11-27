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
        /// Cache containg attempted solutions
        /// </summary>
        public static Dictionary<string, string> strCache;

        /// <summary>
        /// Cache containing integer solutions
        /// </summary>
        public static Dictionary<string, int> iCache;

        /// <summary>
        /// Instance variable for finding solution
        /// </summary>
        public static int[] currList;

        /// <summary>
        /// Main method used for solving the Spiderman's Workout Kattis problem.
        /// </summary>
        /// <param name="args">UNUSED</param>
        static void Main(string[] args)
        {
            string currLine = "";
            exercises = new HashSet<int[]>();
            strCache = new Dictionary<string, string>();
            iCache = new Dictionary<string, int>();

            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numExercises);

            // Reads all input
            BuildExercises(numExercises);

            // TODO extract this to helper method
            // Iterate over our inputs and calculate solution
            foreach (int[] list in exercises)
            {
                // TODO Use dynamic programming to calculate this
                // The hints on the assignment page, seem to be helpful.
                currList = list;
                solutions.Add(strCache["0 0"]);
            }

            foreach(string s in solutions)
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

        public static int RecursiveCall(int currHeight, int idx, int minOfMaxHeight, bool wentUp)
        {
            string key = idx + " " + currHeight;

            // if we are at our last index take current position and minus end, if we don't equal 0 then impossible
            if (idx == currList.Length - 1 && currHeight - currList[idx] != 0)
            {
                // Cache impossible
                if(strCache.ContainsKey(key))
                {
                    strCache[key] = "IMPOSSIBLE";
                    iCache[key] = -99;
                }
                else
                {
                    strCache.Add(key, "IMPOSSIBLE");
                    iCache.Add(key, -99);
                }
                return int.MaxValue;
            }
            // Else pre-pend the 0 to our solution string
            else
            {
                if(strCache.ContainsKey(key))
                {
                    strCache[key] = "D";
                    iCache[key] = minOfMaxHeight;
                }
                else
                {
                    strCache.Add(key, "D");
                    iCache.Add(key, minOfMaxHeight);
                }
                return minOfMaxHeight;
            }

            // Calculate min of up and down
            // recurse on idx + 1, minOfMaxHeight, wentUp = true for up false for down, max height so far 
            // The key for our cache on index and current height and our minOfMaxHeight should be our value
            // When we get the min we pre-pend u or d to our string
            // Cache on recursive calls in both integer cache and string cache
        }
    }
}
