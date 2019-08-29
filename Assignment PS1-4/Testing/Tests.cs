using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Assignment_PS1_4;
using System.Diagnostics;

namespace Assignment_PS1_4
{
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// The minimum duration of a timing eperiment (in msecs) in versions 4 and 5
        /// </summary>
        public const int DURATION = 1000;

        [TestMethod]
        public void TimingNumOfWords()
        {
            for (int i = 100; i <= 1000; i += 100)
            {
                Console.WriteLine("Time " + i + " Words");
                Console.WriteLine("--------------");
                timeTests(i, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\5LetterWords.txt");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        [TestMethod]
        public void TimingK()
        {
            Console.WriteLine("Time k = 3");
            Console.WriteLine("--------------");
            timeTests(2000, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\3LetterWords.txt");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Time k = 4");
            Console.WriteLine("--------------");
            timeTests(2000, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\4LetterWords.txt");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Time k = 5");
            Console.WriteLine("--------------");
            timeTests(2000, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\5LetterWords.txt");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Time k = 6");
            Console.WriteLine("--------------");
            timeTests(2000, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\6LetterWords.txt");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Time k = 7");
            Console.WriteLine("--------------");
            timeTests(2000, @"C:\Users\evanv\source\repos\CS4150Assignments\Assignment PS1-4\Testing\7LetterWords.txt");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private void runTest(int numOfWords, string filePath)
        {
            string[] arr = new string[numOfWords + 1];
            arr[0] = numOfWords + " 5";
            StreamReader file = new StreamReader(@filePath);

            for (int i = 1; i < numOfWords; i++)
            {
                arr[i] = file.ReadLine();
            }

            Program.Main(arr);
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        private static double msecs(Stopwatch sw)
        {
            return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
        }

        public void timeTests(int words, string filePath)
        {
            // Create a stopwatch
            Stopwatch sw = new Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                    runTest(words, @filePath);
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions;

            // Create a stopwatch
            sw = new Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (int i = 0; i < repetitions; i++)
                {
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions;

            // Display the raw data as a sanity check
            Console.WriteLine("Total avg:    " + totalAverage.ToString("G2"));
            Console.WriteLine("Overhead avg: " + overheadAverage.ToString("G2"));
        }
    }
}
