using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    class Program
    {
        public static Dictionary<int, LinkedList<List<int>>> uniqueTrees = new Dictionary<int, LinkedList<List<int>> >();
        static int NumberOfUnique = 0;
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

            Console.WriteLine(NumberOfUnique);
            Console.ReadLine();
        }

        public static void Ceiling(string s)
        {
            int[] nodes = s.Split(new[] {'\t', ' ' }).Select(n => Convert.ToInt32(n)).ToArray();

            // makes a binary search Tree Array
            List<int> binaryTreeArray = MakeBinarytree(nodes);

            // add unique trees to uniqueTrees (dictionary)
            if (uniqueTrees.ContainsKey(binaryTreeArray.Count))
            {
                bool exit = false;
                bool addList = false;

                foreach (List<int> item in uniqueTrees[binaryTreeArray.Count])
                {
                    if(exit == true)
                    {
                        break;
                    }

                    for (int i = 0; i < item.Count; i++)
                    {
                        if( (binaryTreeArray[i] != 0 && item[i] != 0) || (binaryTreeArray[i] == 0 && item[i] == 0))
                        {
                            // do nothing
                        }
                        else
                        {
                            
                            exit = true;
                            addList = true;
                            break;
                        }
                    }
                }
                if(addList == true)
                {
                    uniqueTrees[binaryTreeArray.Count].AddLast(binaryTreeArray);
                    NumberOfUnique++;
                }
            }
            else
            {
                LinkedList<List<int>> linked = new LinkedList<List<int>>();
                linked.AddLast(binaryTreeArray);
                uniqueTrees.Add(binaryTreeArray.Count, linked);
                NumberOfUnique++;
            }
        }

        public static List<int> MakeBinarytree(int[] nodes)
        {
            List<int> tree = new List<int>();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (i == 0)
                {
                    tree.Add(nodes[0]);
                }
                else if(nodes[i] < tree[0])
                {
                    AddHeap(2 * 0 + 1, ref tree, nodes[i]);
                }
                else
                {
                    AddHeap(2 * 0 + 2, ref tree, nodes[i]);
                }
            }
                return tree;
        }

        public static void AddHeap(int index, ref List<int> tree, int number)
        {
            if(tree.Count <= index)
            {
                if(index == tree.Count)
                {
                    tree.Add(number);
                }
                else
                {
                    int emptySpots = index - tree.Count;
                    for (int j = 0; j < emptySpots; j++)
                    {
                        tree.Add(0);
                    }
                    tree.Add(number);
                }
                
            }
            else if(tree.Count > index && tree[index] == 0)
            {
                tree[index] = number;
            }
            else
            {
                if(number < tree[index])
                {
                    index = 2 * index + 1;
                    AddHeap(index, ref tree, number);
                }
                else
                {
                    index = 2 * index + 2;
                    AddHeap(index, ref tree, number);
                }
            }
        }
    }
}