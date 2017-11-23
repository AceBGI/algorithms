using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw10
{
    public class elements
    {
        int r;
        int uncloseableRoom;
        int k;

        public elements(int row, int u, int kRooms)
        {
            r = row;
            uncloseableRoom = u;
            k = kRooms;
        }
    }

    class Program
    {
        public static int[,] values;
        public static Dictionary<elements, int> cache = new Dictionary<elements, int>();

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
                values = new int[N, 2];

                for (int i = 0; i < N; i++)
                {
                    string line = Console.ReadLine();
                    string[] lineValues = line.Split(new[] { '\t', ' ' });
                    int c0;
                    int c1;
                    Int32.TryParse(lineValues[0], out c0);
                    Int32.TryParse(lineValues[1], out c1);

                    values[i, 0] = c0;
                    values[i, 1] = c1;
                }

                int answer = maxValue(0,-1,K,N);
                Console.WriteLine(answer);
            }
        }

        private static int maxValue(int r, int uncloseableRoom, int k, int N)
        {
            int answer = 0;

            if (values.Length / 2 <= r)
            {
                return answer;
            }

            if (cache.TryGetValue(new elements(r, uncloseableRoom, k), out answer))
            {
                return answer;
            }

            // k <= N - r
            if (k == N - r)
            {
                if (uncloseableRoom == 0)
                {
                    answer = values[r,0] + maxValue(r + 1, 0, k - 1, N);
                }
                else if (uncloseableRoom == 1)
                {
                    answer = values[r, 1] + maxValue(r + 1, 1, k - 1, N);
                }
                else if (uncloseableRoom == -1)
                {
                    answer = Math.Max(values[r,0] + maxValue(r + 1, 0, k - 1, N), values[r,1] + maxValue(r + 1, 1, k - 1, N));
                }
            }
            else if (k < N - r)
            {
                if (uncloseableRoom == 0)
                {
                    answer = Math.Max(values[r, 0] + maxValue(r + 1, 0, k - 1, N), values[r, 0] + values[r, 1] + maxValue(r + 1, -1, k, N));
                }
                else if (uncloseableRoom == 1)
                {
                    answer = Math.Max(values[r,1] + maxValue(r + 1, 1, k - 1, N), values[r,1] + values[r,0] + maxValue(r + 1, -1, k, N));
                }
                else if (uncloseableRoom == -1)
                {
                    int tempValue = Math.Max(values[r,0] + maxValue(r + 1, 0, k - 1, N), values[r,1] + maxValue(r + 1, 1, k - 1, N));
                    answer = Math.Max(tempValue, (values[r, 0] + values[r, 1] + maxValue(r + 1, -1, k, N)));
                }
            }

            cache[new elements(r, uncloseableRoom, k)] = answer;

            return answer;
        }

        // values[r][0]+maxValue(r+1,0,k-1)

        // int[, ,] max = new int[N,3,k];
        // for (int i = 2; i < N; i++)
        //     {
        // 
        //         for (int k = 0; k < K; k++)
        //         {
        // 
        //             Max(max[0, 0, k], max[0, 1, k], max[0, -1, k]);
        //         }
        //     }





        //public static int Max(int x, int y)
        //{
        //    return Math.Max(x, y);
        //}
        //
        //public static int Max(int x, int y, int z)
        //{
        //    // Or inline it as x < y ? (y < z ? z : y) : (x < z ? z : x);
        //    // Time it before micro-optimizing though!
        //    return Math.Max(x, Math.Max(y, z));
        //}
        //
        //public static int Max(int w, int x, int y, int z)
        //{
        //    return Math.Max(w, Math.Max(x, Math.Max(y, z)));
        //}
        //
        //public static int Max(params int[] values)
        //{
        //    return Enumerable.Max(values);
        //}
    }

   
}
