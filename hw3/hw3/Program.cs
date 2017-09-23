using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace hw3
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree galaxyTree = new Tree();
            Node root = null;
            bool hasHalf = false;

            string firstLine = Console.ReadLine();
            string[] numbers = firstLine.Split(new[] { '\t', ' ' });
            BigInteger d = BigInteger.Parse(numbers[0]);
            int k;
            Int32.TryParse(numbers[1], out k);

            for (int i = 0; i < k; i++)
            {
                string line = Console.ReadLine();
                Star s = Cordnates(line);

                if(galaxyTree.Count == 0)
                {
                    root = galaxyTree.insert(root, s, d, ref hasHalf, k);
                }
                else if(hasHalf == true)
                {
                    if(((s.X - galaxyTree.megaGalaxy.LeadStar.X) * (s.X - galaxyTree.megaGalaxy.LeadStar.X)) + ((s.Y - galaxyTree.megaGalaxy.LeadStar.Y) * (s.Y - galaxyTree.megaGalaxy.LeadStar.Y)) <= d * d)
                        {
                            galaxyTree.megaGalaxy.Count++;
                        }
                }
                else
                {
                    galaxyTree.insert(root, s, d, ref hasHalf, k);
                }
            }
            if(hasHalf == true)
                Console.WriteLine(galaxyTree.megaGalaxy.Count);
            else
                Console.WriteLine("NO");
            Console.ReadLine();
        }

        static Star Cordnates(string line)
        {
            string[] numbers = line.Split(new[] { '\t', ' ' });
            BigInteger x = BigInteger.Parse(numbers[0]);
            BigInteger y = BigInteger.Parse(numbers[1]);

            Star newStar = new Star();
            newStar.X = x;
            newStar.Y = y;
            newStar.Pos = ((newStar.X - 0) * (newStar.X - 0)) + ((newStar.Y - 0) * (newStar.Y - 0));
            return newStar;
        }
    }

    class Galaxy
    {
        public int Count { get; set; }
        public Star LeadStar { get; set; }
        public int index { get; set; }
    }

    class Star
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
        public BigInteger Pos { get; set; }
    }

    class Node
    {
        public Galaxy data;
        public Node left;
        public Node right;
    }

    class Tree
    {
        public int Count { get; set; }
        public Galaxy megaGalaxy { get; set; }

        public Node insert(Node root, Star s, BigInteger d, ref bool hasHalf, int k)
        {
            if (root == null)
            {
                root = new Node();
                Galaxy newGalaxy = new Galaxy();
                newGalaxy.LeadStar = s;
                newGalaxy.index = Count;
                newGalaxy.Count++;
                root.data = newGalaxy;
                Count++;
            }
            else if (((s.X - root.data.LeadStar.X) * (s.X - root.data.LeadStar.X)) + ((s.Y - root.data.LeadStar.Y) * (s.Y - root.data.LeadStar.Y)) <= d * d)
            {
                root.data.Count++;
                if(root.data.Count > k / 2)
                {
                    hasHalf = true;
                    megaGalaxy = root.data;
                }
                return root;
            }
            else if (s.Pos < root.data.LeadStar.Pos)
            {
                root.left = insert(root.left, s, d, ref hasHalf, k);
            }
            else
            {
                root.right = insert(root.right, s, d, ref hasHalf, k);
            }

            return root;
        }
    }
}
