using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Anaga("6 4\ntape\nrate\nseat\npate\neast\npest");

            Console.WriteLine(count);
            Console.ReadLine();
        }

        public static int Anaga(string input)
        {
            input.Trim();
            string[] wordArray = input.Split(new[] { '\n', '\t', ' ' });
            int n;
            int k;
            Int32.TryParse(wordArray[0], out n);
            Int32.TryParse(wordArray[1], out k);

            Dictionary<int, string> words = new Dictionary<int, string>();
            List<string> solutions = new List<string>();
            List<string> rejected = new List<string>();


            for (int i = 2; i < wordArray.Length; i++)
            {
                words.Add(i - 2,wordArray[i]);
            }

            foreach (string word in words.Values)
            {
                char[] w = word.ToCharArray();
                Array.Sort(w);
                string sortedWord = new string(w); ;

                if (solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (rejected.Contains(sortedWord) == false)
                {
                    solutions.Add(sortedWord);
                }

            }
            return solutions.Count;
        }

    }
}
