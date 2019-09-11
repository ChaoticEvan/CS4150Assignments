using System;

namespace PS3_6
{
    class Program
    {
        public static int diameter;
        static void Main(string[] args)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineTokens = firstLine.Split(' ');
            diameter = -99;
            int numLines = -99;

            Int32.TryParse(firstLineTokens[0], out int i);
            diameter = i;


            Int32.TryParse(firstLineTokens[1], out int j);
            numLines = j;

            Star[] stars = new Star[numLines];

            string currLine = "";
            for (int idx = 0; idx < numLines; idx++)
            {
                currLine = Console.ReadLine();

                string[] currLineTokens = currLine.Split(' ');
                stars[idx] = new Star(Int32.Parse(currLineTokens[0]), Int32.Parse(currLineTokens[1]));
            }

            Star result = findMajority(stars, 0, stars.Length - 1, out int candidate);

            if (candidate == 0)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine(candidate.ToString());
            }
        }

        private static Star findMajority(Star[] stars, int lo, int hi, out int result)
        {
            if (hi - lo == 0)
            {
                result = 0;
                return null;
            }
            else if (hi - lo == 1)
            {
                result = 1;
                return stars[0];
            }
            else
            {
                Star x = findMajority(stars, lo, (hi + lo) / 2, out int xCount);
                Star y = findMajority(stars, ((hi + lo) / 2) + 1, hi, out int yCount);
                if (xCount == 0 && yCount == 0)
                {
                    result = 0;
                    return null;
                }
                else if (xCount == 0)
                {
                    result = count(stars, lo, hi, y);
                    if (result > (hi - lo) / 2)
                    {
                        return y;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (yCount == 0)
                {
                    result = count(stars, lo, hi, x);
                    if (result > (hi - lo) / 2)
                    {
                        return x;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    int xRes = count(stars, lo, hi, x);
                    int yRes = count(stars, lo, hi, y);

                    if (xRes > hi - lo/ 2)
                    {
                        result = xRes;
                        return x;
                    }
                    else if (yRes > hi - lo/ 2)
                    {
                        result = yRes;
                        return y;
                    }
                    else
                    {
                        result = 0;
                        return null;
                    }
                }
            }
        }

        private static int count(Star[] stars, int lo, int hi, Star candidate)
        {
            if(candidate == null)
            {
                return 0;
            }
            int result = 0;
            for (int idx = lo; idx <= hi; idx++)
            {
                bool cond = Math.Pow((candidate.x - stars[idx].x), 2) + Math.Pow(candidate.y - stars[idx].y, 2) <= Math.Pow(diameter, 2);
                if (cond)
                {
                    result++;
                }
            }

            if (result > (hi - lo) / 2)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }

    class Star
    {
        public int x { get; set; }
        public int y { get; set; }

        public Star(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }

}
