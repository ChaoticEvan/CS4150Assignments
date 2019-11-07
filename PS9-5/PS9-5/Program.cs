using System;

namespace PS9_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32.TryParse(Console.ReadLine(), out int numOfLines);
            
            // Using numOfLines + 1 and Less then or equal to num of lines
            // Because for some reason the number of lines is + 1 of the given number
            int[] distance = new int[numOfLines + 1];
            for(int i = 0; i <= numOfLines; ++i)
            {
                Int32.TryParse(Console.ReadLine(), out int temp);
                distance[i] = temp;
            }
            return;
        }
    }
}
