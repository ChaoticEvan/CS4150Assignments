using System;
using System.Collections.Generic;

namespace PS9_5
{
    class Program
    {
        private static int[] distance;
        private static Dictionary<int, double> costs;
        static void Main(string[] args)
        {
            costs = new Dictionary<int, double>();
            Int32.TryParse(Console.ReadLine(), out int numOfLines);

            // Using numOfLines + 1 and Less then or equal to num of lines
            // Because for some reason the number of lines is + 1 of the given number
            distance = new int[numOfLines + 1];
            for (int i = 0; i <= numOfLines; ++i)
            {
                Int32.TryParse(Console.ReadLine(), out int temp);
                distance[i] = temp;
            }

            for (int i = numOfLines - 1; i >= 0; --i)
            {
                double currCost = Math.Pow(400 - (distance[i + 1] - distance[i]), 2);
                if (costs.ContainsKey(distance[i]) && costs[distance[i]] > currCost)                
                {
                    costs[distance[i]] = currCost;
                }
                else
                {
                    costs.Add(distance[i], currCost);
                }
            }
            return;
        }
    }
}
