using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw9
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> penalty = new HashSet<int>();
            List<int> dist = new List<int>();

            string firstLine = Console.ReadLine();
            int N;
            Int32.TryParse(firstLine, out N);
            N++;

            for (int i = 0; i < N; i++)
            {
                string line = Console.ReadLine();
                int d;
                Int32.TryParse(line, out d);

                dist.Add(d);
            }

            // penalty(i) = 400 - (dist(k) - dist(i)) ^ 2 + penalty(k)
        }
    }
}
