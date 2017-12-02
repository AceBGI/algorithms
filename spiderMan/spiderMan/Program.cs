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
            List<string> output = new List<string>();

            string firstLine = Console.ReadLine();
            int N;
            Int32.TryParse(firstLine, out N);

            for (int i = 0; i < N; i++)
            {
                string lineNum = Console.ReadLine();
                int M;
                Int32.TryParse(lineNum, out M);

                int[] distances = new int[M+1];
                int[,] answers = new int[41,1001];
                for (int u = 0; u < 41; u++)
                {
                    for (int v = 0; v < 1001; v++)
                    {
                        answers[u,v] = -1;
                    }
                }
                int[,] direction = new int[41, 1001];
                Array.Clear(direction, 0, direction.Length);

                string line = Console.ReadLine();
                string[] lineD = line.Split(new[] { '\t', ' ' });
                int[] DistanceNumbers = Array.ConvertAll(lineD, int.Parse);
                for (int j = 0; j < DistanceNumbers.Length; j++)
                {
                    distances[j] = DistanceNumbers[j];
                }


                output.Add( workOut(distances, answers, direction, M, up, down) );
            }
            foreach (var item in output)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        private static string workOut(int[] distances, int[,] answer, int[,] direction, int M, int up, int down)
        {
            answer[0, 0] = 0;
            for (int i = 1; i <= M; i++)
                {
                for (int j = 0; j <= 1000 - distances[i]; j++)
                {
                    up = j + distances[i];
                    down = j - distances[i];

                    // you can not go underground, down != -number
                    if(down >= 0)
                    {
                        if (answer[i - 1, up] != -1 && answer[i - 1, down] != -1)
                        {
                            answer[i, j] = Math.Min(answer[i - 1, up], Math.Max(j, answer[i - 1, down]));

                            if (answer[i - 1, up] > Math.Max(j, answer[i - 1, down]))
                            {
                                direction[i, j] = 1;
                            }
                            else
                            {
                                direction[i, j] = -1;
                            }
                        }
                        else if (answer[i - 1, up] != -1)
                        {
                            answer[i, j] = answer[i - 1, up];
                            direction[i, j] = -1;
                        }
                        else if(answer[i-1,down] != -1)
                        {
                            answer[i, j] = Math.Max(j, answer[i-1,down]);
                            direction[i, j] = 1;
                        }
                    }
                    else if (answer[i-1,up] != -1)
                    {
                        answer[i, j] = answer[i - 1, up];
                        direction[i, j] = -1;
                    }
                }
            }

            int[] solution = new int[M + 1];
            int index = 0;
            for (int i = M; i >= 1; i--)
            {
                solution[i] = direction[i,index];
                index = index - direction[i, index] * distances[i];
            }

            string temp = "";
            if(answer[M,0] != -1)
            {
                string t = "";
                for (int i = 1; i <= M; i++)
                {
                    if(solution[i] == 1)
                    {
                        t += "U";
                    }
                    else
                    {
                        t += "D";
                    }
                }
                temp = t;
            }
            else
            {
                temp = "IMPOSSIBLE";
            }

            return temp;
        }
    }
}
