using System;
using System.Collections.Generic;

namespace PS11_6
{
    class Program
    {
        public static HashSet<List<int>> exercises;
        static void Main(string[] args)
        {
            string currLine = "";
            exercises = new HashSet<List<int>>();

            currLine = Console.ReadLine();
            Int32.TryParse(currLine, out int numExercises);
            BuildExercises(numExercises);
            return;
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
                List<int> temp = new List<int>();
                for (int j = 0; j < numSteps; ++j)
                {
                    temp.Add(Int32.Parse(currLineTokens[j]));
                }

                // Add our entry into our dictionary to do the work on later.
                // The length of temp is going to be our number of steps
                exercises.Add(temp);
            }
        }
    }
}
