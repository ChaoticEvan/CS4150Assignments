using System;
using System.Collections.Generic;
using System.Text;

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
            double currentOutput = findCheapestPath(0);
            Console.WriteLine(currentOutput);
        }

        private static double findCheapestPath(int idx)
        {
            if (idx == distance.Length - 1)
            {
                return costs[distance[idx]];
            }

            MinHeap minHeap = new MinHeap(distance.Length - idx);

            for (int i = idx; idx < distance.Length; ++i)
            {
                double tempCost = Math.Pow(400 - (distance[idx + 1] - distance[idx]), 2);
                minHeap.Add(tempCost);
            }

            return minHeap.Pop() + findCheapestPath(idx++);
        }
    }

    /// <summary>
    /// I grabbed this min heap implementation from the link below
    /// https://egorikas.com/max-and-min-heap-implementation-with-csharp/
    /// </summary>
    public class MinHeap
    {
        private readonly double[] _elements;
        private int _size;

        public MinHeap(int size)
        {
            _elements = new double[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private double GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private double GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private double GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public double Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public double Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(double element)
        {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException();

            _elements[_size] = element;
            _size++;

            ReCalculateUp();
        }

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex] >= _elements[index])
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[index] < GetParent(index))
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}
