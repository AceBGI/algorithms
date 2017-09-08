using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    class Program
    {
        public static Dictionary<int, List<int>> uniqueTrees = new Dictionary<int, List<int>>();
        static void Main(string[] args)
        {
            List<string> treeArray = new List<string>();
            string firstLine = Console.ReadLine();
            string[] numbers = firstLine.Split(new[] { '\t', ' ' });
            int n;
            int k;
            Int32.TryParse(numbers[0], out n);
            Int32.TryParse(numbers[1], out k);

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                treeArray.Add(line);
            }

            foreach (string item in treeArray)
            {
                Ceiling(item);
            }

            Console.WriteLine(uniqueTrees.Count);
            Console.ReadLine();
        }

        public static void Ceiling(string s)
        {
            int[] nodes = s.Split(new[] {'\t', ' ' }).Select(n => Convert.ToInt32(n)).ToArray();

            // makes a binary search Tree Array
            List<int> binaryTreeArray = BinarySearchTree(nodes);

            // add unique trees to uniqueTrees (dictionary)
            if (uniqueTrees.ContainsKey(binaryTreeArray.Count))
            {
                // do nothing ... you may have to do something here. Hopefully not!!!?
            }
            else
            {
                uniqueTrees.Add(binaryTreeArray.Count, binaryTreeArray);
            }
        }

        private static List<int> BinarySearchTree(int[] nodes)
        {
            List<int> tree = new List<int>();
            int pointer = 0;
            int Left = 0;
            int right = 0;

            for (int i = 0; i < nodes.Length; i++)
            {
                if (i == 0)
                {
                    tree.Add(nodes[0]);
                }
                else
                {
                    if ( (nodes[i] > tree[pointer] && right == 1) || ( nodes[i] < tree[pointer] && Left == 1) )
                    {
                        pointer = tree.Count - 1;
                        Left = 0;
                        right = 0;
                    }

                    if (nodes[i] < tree[pointer]) //left
                    {
                        int indexNumber = 2 * pointer + 1;
                        int emptySpots;
                        if (indexNumber == tree.Count)
                        {
                            emptySpots = 0;
                        }
                        else
                        {
                            emptySpots = indexNumber - tree.Count;
                        }

                        if (indexNumber < tree.Count)
                        {
                            tree[indexNumber]  = nodes[i];
                        }
                        else
                        {
                            for (int j = 0; j < emptySpots; j++)
                            {
                                tree.Add(0);
                            }
                            tree.Add(nodes[i]);
                        }
                        Left++;
                    }
                    else // right
                    {
                        int indexNumber = 2 * pointer + 2;
                        int emptySpots = indexNumber - tree.Count;
                        for (int j = 0; j < emptySpots; j++)
                        {
                            tree.Add(0);
                        }
                        tree.Add(nodes[i]);
                        right++;
                    }
                }
            }

            // return a binary Tree Array
            return tree;
        }
    }
}
