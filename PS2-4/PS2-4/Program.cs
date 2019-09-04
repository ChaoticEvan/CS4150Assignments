using System;
using System.Collections.Generic;
using System.Text;

namespace PS2_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineTokens = firstLine.Split(' ');
            int numLines = -99;
            int numNumber = -99;

            Int32.TryParse(firstLineTokens[0], out int i);
            numLines = i;


            Int32.TryParse(firstLineTokens[1], out int j);

            numNumber = j;


            HashSet<Tree> bsts = new HashSet<Tree>();
            string currLine = "";
            for (int idx = 0; idx < numLines; idx++)
            {
                currLine = Console.ReadLine();


                Tree bst = new Tree();
                string[] currLineTokens = currLine.Split(' ');
                for (int currNum = 0; currNum < numNumber; currNum++)
                {
                    Int32.TryParse(currLineTokens[currNum], out int k);
                    bst.insert(k);

                }
                bsts.Add(bst);
            }

            HashSet<string> patterns = new HashSet<string>();
            foreach (Tree currTree in bsts)
            {
                currTree.traverse();
                patterns.Add(currTree.pattern.ToString());
            }

            Console.WriteLine(patterns.Count);
        }
    }

    class Node
    {
        public int value;
        public Node left;
        public Node right;
    }

    class Tree
    {
        public Node root { private set; get; }
        public StringBuilder pattern { private set; get; }

        public Tree()
        {
            pattern = new StringBuilder();
        }

        public void insert(int v)
        {
            if (this.root == null)
            {
                this.root = new Node();
                this.root.value = v;
            }
            else if (v < this.root.value)
            {

                this.root.left = insertRec(this.root.left, v);
            }
            else
            {
                this.root.right = insertRec(this.root.right, v);
            }
        }

        private Node insertRec(Node curr, int v)
        {
            if (curr == null)
            {
                curr = new Node();
                curr.value = v;
            }
            else if (v < curr.value)
            {

                curr.left = insertRec(curr.left, v);
            }
            else
            {
                curr.right = insertRec(curr.right, v);
            }

            return curr;
        }

        public void traverse()
        {
            if (this.root == null)
            {
                return;
            }
            if (this.root != null)
            {
                pattern.Append("Root");
            }
            if (this.root.left != null)
            {
                pattern.Append("L");
            }
            if (this.root.right != null)
            {
                pattern.Append("R");
            }

            traverseRec(this.root.left);
            traverseRec(this.root.right);
        }

        private void traverseRec(Node curr)
        {

            if (curr == null)
            {
                return;
            }

            if (curr.left != null)
            {
                pattern.Append("L");
            }
            if (curr.right != null)
            {
                pattern.Append("R");
            }
            traverseRec(curr.left);
            traverseRec(curr.right);
        }
    }
}