using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderMan
{
    class Program
    {
        static void Main(string[] args)
        {
            // var
            int up = 0;
            int down = 0;

            string firstLine = Console.ReadLine();
            int N;
            Int32.TryParse(firstLine, out N);

            for (int i = 0; i < N; i++)
            {
                string lineNum = Console.ReadLine();
                int M;
                Int32.TryParse(lineNum, out M);

                int[] distances = new int[M+1];
                int[,] a = new int[41,1001];
                int[,] direction = new int[41, 1001];
                Array.Clear(direction, 0, direction.Length);

                string line = Console.ReadLine();
                string[] lineD = line.Split(new[] { '\t', ' ' });
                distances = Array.ConvertAll(lineD, int.Parse);

                


                workOut(distances, a, direction, M, up, down);


            }
        }

        private static void workOut(int[] distances, int[,] a, int[,] direction, int M, int up, int down)
        {
            a[0, 0] = 0;
            for (int i = 0; i <= M; i++)
            {
                for (int j = 0; j <= 1000 - distances[i]; j++)
                {
                    up = j + distances[i];
                    down = j - distances[i];

                    // you can not go underground, down != -number
                    if(down >= 0)
                    {

                    }
                    else if (a[i-1,up] != -1)
                    {
                        a[i, j] = a[i - 1, up];
                        direction[i, j] = -1;
                        //https://github.com/meysamaghighi/Kattis/blob/master/Spiderman's%20workout/a.cpp
                    }
                }
            }
        }
    }
}
