using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Assignment_PS1_4
{
    class Timing
    {
        /// <summary>
        /// The minimum duration of a timing eperiment (in msecs) in versions 4 and 5
        /// </summary>
        public const int DURATION = 1000;

        static void Main(string[] args)
        {
            // Report the average time required to do a linear search for various sizes
            // of arrays.
            int size = 32;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 17; i++)
            {
                size = size * 2;
                double currentTime = TimeLinearSearch1(size - 1);
                Console.Write((size - 1) + "\t" + currentTime.ToString("G3"));
                if (i > 0)
                {
                    Console.WriteLine("   \t" + (currentTime / previousTime).ToString("G3"));
                }
                else
                {
                    Console.WriteLine();
                }
                previousTime = currentTime;
            }
        }

        public static double TimeLinearSearch1(int size)
        {
            // Construct a sorted array
            int[] data = new int[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = i;
            }

            // Get the process
            Process p = Process.GetCurrentProcess();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        Program.Main(new string[1]);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / size;

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;
        }
    }
}
