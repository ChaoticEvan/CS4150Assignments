using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_PS1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> dictionary = new HashSet<string>();
            HashSet<string> approved = new HashSet<string>();
            HashSet<string> rejected = new HashSet<string>();

            // Read the first line and parse the ints
            string firstLine = Console.ReadLine();
            List<int> firstLineNums = new List<int>();
            foreach (string num in firstLine.Split(new char[] { ' ' }))
            {
                if (Int32.TryParse(num, out int result))
                {
                    firstLineNums.Add(result);
                }
            }

            // Read through the rest of the lines to create our "dictionary"
            string line;
            for(int i = 0; i < firstLineNums[0]; i++)
            {
                line = Console.ReadLine();
                dictionary.Add(line);
            }

            // count/print anagrams
            foreach (string word in dictionary)
            {
                string sortedWord = String.Concat(word.OrderBy(c => c));

                if (approved.Contains(sortedWord))
                {
                    approved.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (!rejected.Contains(sortedWord))
                {
                    approved.Add(sortedWord);
                }
            }

            Console.WriteLine(approved.Count);
        }
    }
}
