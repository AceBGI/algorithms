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
            List<Galaxy> galaxyArray = new List<Galaxy>();
            bool hasHalf = false;
            int theChoosenOne = 0;

            string firstLine = Console.ReadLine();
            string[] numbers = firstLine.Split(new[] { '\t', ' ' });
            BigInteger d = BigInteger.Parse(numbers[0]);
            int k;
            Int32.TryParse(numbers[1], out k);

            for (int i = 0; i < k; i++)
            {
                string line = Console.ReadLine();
                Star s = Cordnates(line);

                if(galaxyArray.Count == 0)
                {
                    Galaxy newGalaxy = new Galaxy();
                    newGalaxy.LeadStar = s;
                    newGalaxy.Count++;
                    galaxyArray.Add(newGalaxy);
                }
                else
                {
                    bool hasGalaxy = false;
                    int index = 0;
                    foreach (Galaxy gal in galaxyArray)
                    {
                        if ( ((s.X - gal.LeadStar.X)*(s.X - gal.LeadStar.X)) + ((s.Y - gal.LeadStar.Y)*(s.Y - gal.LeadStar.Y)) <= d * d)
                        {
                            gal.Count++;
                            hasGalaxy = true;

                            if(gal.Count > k/2)
                            {
                                hasHalf = true;
                                theChoosenOne = index;
                            }
                        }
                        index++;
                    }

                    if(hasGalaxy == false)
                    {
                        Galaxy newGalaxy = new Galaxy();
                        newGalaxy.LeadStar = s;
                        newGalaxy.Count++;
                        galaxyArray.Add(newGalaxy);
                    }
                }
            }
            if(hasHalf == true)
                Console.WriteLine(galaxyArray[theChoosenOne].Count);
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
            return newStar;
        }
    }

    class Galaxy
    {
        public int Count { get; set; }
        public Star LeadStar { get; set; }
    }

    class Star
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }
    }
}
