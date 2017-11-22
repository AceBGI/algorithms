using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw10
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                string firstLine = Console.ReadLine();
                string[] fLine = firstLine.Split(new[] { '\t', ' ' });
                int N;
                int K;
                Int32.TryParse(fLine[0], out N);
                Int32.TryParse(fLine[1], out K);

                if (N == 0 && K == 0)
                    loop = false;

                for (int i = 0; i < N; i++)
                {
                    string line = Console.ReadLine();
                    string[] lineValues = line.Split(new[] { '\t', ' ' });
                    int c0;
                    int c1;
                    Int32.TryParse(lineValues[0], out c0);
                    Int32.TryParse(lineValues[1], out c1);
                }

                search(N, K);
            }
        }

        private static void search(int N, int K)
        {
            int[, ,] max = new int[N,3,K];

            for (int i = 2; i < N; i++)
            {

                for (int k = 0; k < K; k++)
                {

                    Max(max[0, 0, k], max[0, 1, k], max[0, -1, k]);
                }
            }
        }

        // values[r][0]+maxValue(r+1,0,k-1)






        public static int Max(int x, int y)
        {
            return Math.Max(x, y);
        }

        public static int Max(int x, int y, int z)
        {
            // Or inline it as x < y ? (y < z ? z : y) : (x < z ? z : x);
            // Time it before micro-optimizing though!
            return Math.Max(x, Math.Max(y, z));
        }

        public static int Max(int w, int x, int y, int z)
        {
            return Math.Max(w, Math.Max(x, Math.Max(y, z)));
        }

        public static int Max(params int[] values)
        {
            return Enumerable.Max(values);
        }
    }

   
}
