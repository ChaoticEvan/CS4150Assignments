using System;
using System.Collections.Generic;
using System.Text;

namespace PS10_4
{
    class Program
    {
        private static Node[,] gallery;
        private static SortedDictionary<int, HashSet<Node>> values;

        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            string[] currLineTokens = currLine.Split(" ");

            Int32.TryParse(currLineTokens[0], out int numOfRows);
            Int32.TryParse(currLineTokens[1], out int numRoomsToClose);
            BuildGallery(numOfRows);

            // TO DO
            // BUILD ALGORITHM TO SOLVE
            int currRoomsClosed = 0;
            foreach (int key in values.Keys)
            {
                foreach (Node node in values[key])
                {
                    if(currRoomsClosed == numRoomsToClose)
                    {
                        break;
                    }
                    Node currNode = gallery[node.row, node.col];

                    if (currNode.canClose && !currNode.isClosed)
                    {
                        currNode.isClosed = true;
                        CloseNeighbors(currNode);
                        ++currRoomsClosed;
                    }
                }
            }

            Console.WriteLine(SumOpenRooms());
        }

        private static void CloseNeighbors(Node currNode)
        {
            // Can't close neighbor in same row
            if (currNode.col == 1)
            {
                gallery[currNode.row, 0].canClose = false;
            }
            else
            {
                gallery[currNode.row, 1].canClose = false;
            }

            // Can't close diagonal above
            if (currNode.row > 0 && currNode.col == 0)
            {
                gallery[currNode.row - 1, 1].canClose = false;
            }
            else if (currNode.row > 0 && currNode.col == 1)
            {
                gallery[currNode.row - 1, 0].canClose = false;
            }

            // Can't close diagonal below
            if (currNode.row < (gallery.Length / 2) - 1 && currNode.col == 0)
            {
                gallery[currNode.row + 1, 1].canClose = false;
            }
            else if (currNode.row < (gallery.Length / 2) - 1 && currNode.col == 1)
            {
                gallery[currNode.row + 1, 0].canClose = false;
            }
        }

        private static void BuildGallery(int numOfRows)
        {
            string currLine = "";
            string[] currLineTokens;
            gallery = new Node[numOfRows, 2];
            values = new SortedDictionary<int, HashSet<Node>>(new DescendingComparer<int>());

            for (int i = 0; i < numOfRows; ++i)
            {
                // Parse the current line and add the nodes to our 2D array
                currLine = Console.ReadLine();
                currLineTokens = currLine.Split(" ");
                int leftVal = Int32.Parse(currLineTokens[0]), rightVal = Int32.Parse(currLineTokens[1]);
                gallery[i, 0] = new Node()
                {
                    value = leftVal,
                    row = i,
                    col = 0,
                    canClose = true,
                    isClosed = false
                };
                gallery[i, 1] = new Node()
                {
                    value = rightVal,
                    row = i,
                    col = 1,
                    canClose = true,
                    isClosed = false
                };

                if (values.ContainsKey(leftVal))
                {
                    values[leftVal].Add(gallery[i, 0]);
                }
                else
                {
                    values[leftVal] = new HashSet<Node>();
                    values[leftVal].Add(gallery[i, 0]);
                }

                if (values.ContainsKey(rightVal))
                {
                    values[rightVal].Add(gallery[i, 1]);
                }
                else
                {
                    values[rightVal] = new HashSet<Node>();
                    values[rightVal].Add(gallery[i, 1]);
                }
            }
        }


        /// <summary>
        /// Method only for testing that the gallery is printing correctly.
        /// </summary>
        private static void PrintGallery()
        {
            StringBuilder sb = new StringBuilder();

            // Divide the length by 2 because we have 2 columns, and
            // the length is the number of elements in all columns and
            // all rows.
            for (int i = 0; i < (gallery.Length / 2); ++i)
            {
                sb.AppendLine(gallery[i, 0].value + " " + gallery[i, 1].value);
            }

            Console.WriteLine(sb.ToString());
        }

        private static int SumOpenRooms()
        {
            int sum = 0;
            // Divide the length by 2 because we have 2 columns, and
            // the length is the number of elements in all columns and
            // all rows.
            for (int i = 0; i < (gallery.Length / 2); ++i)
            {
                if (!gallery[i, 0].isClosed)
                {
                    sum += gallery[i, 0].value;
                }
                if (!gallery[i, 1].isClosed)
                {
                    sum += gallery[i, 1].value;
                }
            }
            return sum;
        }
    }

    /// <summary>
    /// I grabbed this min heap from here: https://egorikas.com/max-and-min-heap-implementation-with-csharp/
    /// </summary>
    public class MinHeap
    {
        private readonly int[] _elements;
        private int _size;

        public MinHeap(int size)
        {
            _elements = new int[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private int GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private int GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private int GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

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

        public int Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public int Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(int element)
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

    public struct Node
    {
        public int value { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public bool canClose { get; set; }
        public bool isClosed { get; set; }

    }

    class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x);
        }
    }
}
