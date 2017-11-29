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
            string firstLine = Console.ReadLine();
            int N;
            Int32.TryParse(firstLine, out N);

            for (int i = 0; i < N; i++)
            {
                string lineNum = Console.ReadLine();
                int Num;
                Int32.TryParse(lineNum, out Num);


            }
            string line = Console.ReadLine();
            string[] lineD = line.Split(new[] { '\t', ' ' });
            int[] distances = Array.ConvertAll(lineD, int.Parse);

            workOut(distances);
        }

        private static void workOut(int[] distances)
        {
            throw new NotImplementedException();
        }
    }
}
