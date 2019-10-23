using System;
using System.Collections.Generic;

namespace PS7_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string currLine = "";
            HashSet<string> results = new HashSet<string>();
            while (!String.IsNullOrEmpty(currLine = Console.ReadLine()))
            {
                string[] currLineTokens = currLine.Split(" ");
                switch (currLineTokens[0])
                {
                    case "gcd":
                        results.Add(gcd(Int32.Parse(currLineTokens[1]), Int32.Parse(currLineTokens[2])).d.ToString());
                        break;
                }
                

            }
        }

        private static GcdContainer gcd(int a, int b)
        {
            GcdContainer result = new GcdContainer();
            if (b == 0)
            {
                result.x = 1;
                result.y = 0;
                result.d = a;
                return result;
            }
            else
            {
                result = gcd(b, a % b);
                return result;
            }
        }

        private struct GcdContainer
        {
            public int x { get; set; }
            public int y { get; set; }
            public int d { get; set; }
        }
    }
}
