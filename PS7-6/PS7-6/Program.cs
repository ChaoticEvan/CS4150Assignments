﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PS7_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string currLine = "";
            List<string> results = new List<string>();

            // Loop through input and perform arithmetic based on given case for each line
            while (!String.IsNullOrEmpty(currLine = Console.ReadLine()))
            {
                string[] currLineTokens = currLine.Split(" ");
                switch (currLineTokens[0])
                {
                    case "gcd":
                        results.Add(gcd(Int32.Parse(currLineTokens[1]), Int32.Parse(currLineTokens[2])).d.ToString());
                        break;

                    case "exp":
                        results.Add(exp(long.Parse(currLineTokens[1]), long.Parse(currLineTokens[2]), long.Parse(currLineTokens[3])).ToString());
                        break;

                    case "inverse":
                        results.Add(inverse(Int32.Parse(currLineTokens[1]), Int32.Parse(currLineTokens[2])));
                        break;

                    case "isprime":
                        results.Add(isPrime(Int32.Parse(currLineTokens[1])));
                        break;

                    case "key":
                        results.Add(key(Int32.Parse(currLineTokens[1]), Int32.Parse(currLineTokens[2])).ToString());
                        break;
                }
            }

            // Print out results
            foreach (string s in results)
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Algorithm for computing GCD from Slides
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Python equivalent of Triplet</returns>
        private static GcdContainer gcd(int a, int b)
        {
            int absA = Math.Abs(a);
            int absB = Math.Abs(b);
            GcdContainer result = new GcdContainer();
            if (absB == 0)
            {
                result.x = 1;
                result.y = 0;
                result.d = a;
                return result;
            }
            else
            {
                GcdContainer temp = gcd(absB, mod(absA, absB));
                result.x = temp.y;
                result.y = temp.x - (absA / absB) * temp.y;
                result.d = temp.d;
                return result;
            }
        }

        /// <summary>
        /// Algorithm for computing exp from Slides
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="N"></param>
        /// <returns>Answer</returns>
        private static long exp(long x, long y, long N)
        {
            if (y == 0)
            {
                return 1;
            }
            else
            {
                long z = exp(x, y / 2, N);
                if (y % 2 == 0)
                {
                    return mod((int)Math.Pow(z, 2), N);
                }
                else
                {
                    return mod((int)(x * Math.Pow(z, 2)), N);
                }
            }
        }

        /// <summary>
        /// Algorithm for computing inverse from Slides
        /// </summary>
        /// <param name="a"></param>
        /// <param name="N"></param>
        /// <returns>Returns inverse if it exists, or returns "none"</returns>
        private static string inverse(int a, int N)
        {
            GcdContainer cont = gcd(a, N);
            if (cont.d == 1)
            {
                return mod(cont.x, N).ToString();
            }
            else
            {
                return "none";
            }
        }

        private static int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private static long mod(long x, long m)
        {
            long r = x % m;
            return r < 0 ? r + m : r;
        }

        /// <summary>
        /// Algorithm for finding if N is Prime from slides
        /// </summary>
        /// <param name="N"></param>
        /// <returns>Returns string yes or no corresponding to if N is prime</returns>
        private static string isPrime(int N)
        {
            bool result = testPrimality(N);
            if (result)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        /// <summary>
        /// Private helper method that runs 100 tests of the primality
        /// test described in the slides
        /// </summary>
        /// <param name="N">Number to test</param>
        /// <returns>bool if number passes all 100 tests</returns>
        private static bool testPrimality(int N)
        {
            double pow2 = Math.Pow(2, N - 1);
            if (pow2 % N != 1)
            {
                return false;
            }

            double pow3 = Math.Pow(3, N - 1);
            if (pow3 % N != 1)
            {
                return false;
            }

            double pow5 = Math.Pow(5, N - 1);
            if (pow5 % N != 1)
            {
                return false;
            }

            return true;
        }


        private static KeyContainer key(int p, int q)
        {
            KeyContainer result = new KeyContainer();
            result.modulus = p * q;
            int fi = (p - 1) * (q - 1);
            result.publicExponent = findE(fi);
            result.privateExponent = Int32.Parse(inverse(result.publicExponent, fi));
            return result;
        }

        private static int findE(int fi)
        {
            for (int i = 2; i < 3120; i++)
            {
                if (gcd(fi, i).d == 1)
                {
                    return i;
                }
            }

            // Should never get here
            return -99;
        }

        /// <summary>
        /// Struct for replicating Python's Triplet
        /// </summary>
        private struct GcdContainer
        {
            public int x { get; set; }
            public int y { get; set; }
            public int d { get; set; }
        }
        /// <summary>
        /// Struct for replicating Python's triplet for the RSA key method
        /// </summary>
        private struct KeyContainer
        {
            public double modulus { get; set; }
            public int publicExponent { get; set; }
            public int privateExponent { get; set; }

            public override string ToString()
            {
                string result = this.modulus + " " + this.publicExponent + " " + this.privateExponent;
                return result;
            }
        }
    }
}
