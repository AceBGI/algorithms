using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5
{
    class Program
    {
        public static Dictionary<string, SortedSet<string>> students = new Dictionary<string, SortedSet<string>>();

        static void Main(string[] args)
        {
            // cities and tolls / vertex
            string studentNumber = Console.ReadLine();
            int n;
            Int32.TryParse(studentNumber, out n);

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                students.Add(line, new SortedSet<string>());
            }

            string numberOffriends = Console.ReadLine();
            int f;
            Int32.TryParse(numberOffriends, out f);

            for (int i = 0; i < f; i++)
            {
                string line = Console.ReadLine();
                string[] sList = line.Split(new[] { '\t', ' ' });
                students[sList[0]].Add(sList[1]);
                students[sList[1]].Add(sList[0]);
            }

            string reportNumber = Console.ReadLine();
            int r;
            Int32.TryParse(reportNumber, out r);

            for (int i = 0; i < r; i++)
            {
                string line = Console.ReadLine();
                Report(line);
            }
        }

        private static void Report(string name)
        {
            throw new NotImplementedException();
        }
    }
}
