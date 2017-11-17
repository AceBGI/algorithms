using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw9
{
    class Program
    {
        public static Dictionary<long, long> cashe = new Dictionary<long, long>();
        static void Main(string[] args)
        {
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

            penalty(dist);

            long answer = cashe.Last().Value;
            Console.WriteLine(answer);
            //Console.ReadLine();
            // penalty(i) = 400 - (dist(k) - dist(i)) ^ 2 + penalty(k)
        }

        private static void penalty(List<int> dist)
        {
            long min = long.MaxValue;
            for (int i = dist.Count - 1; i >= 0; i--)
            {
                min = Math.Min(min, dynamicPenalty(dist, i));
            }
        }

        private static long dynamicPenalty(List<int> dist, int i)
        {
            long min = long.MaxValue;
            long r;
            if (cashe.TryGetValue(i, out r))
            {
                return r;
            }
            else if (dist.Count - 1 == i)
            {
                return 0;
            }
            else
            {
                for (int j = i + 1; j < dist.Count; j++)
                {
                    long penalty = (long)Math.Pow(400 - (dist[j] - dist[i]), 2) + dynamicPenalty(dist, j);
                    min = Math.Min(min, penalty);
                }
                cashe[i] = min;
                return min;
            }
        }

    }
}
