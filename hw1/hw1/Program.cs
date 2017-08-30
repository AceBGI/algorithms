using System;
using System.Collections.Generic;
using System.Text;

namespace hw1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordArray = new List<string>();
            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                wordArray.Add(line);
                line = Console.ReadLine();
            }

            // 6 4
            // tape
            // rate
            // seat
            // pate
            // east
            // pest
            int count = Anaga(wordArray);

            Console.WriteLine(count);
            Console.ReadLine();
        }

        public static int Anaga(List<string> wordArray)
        {
            string[] numbers = wordArray[0].Split(new[] { '\n', '\t', ' ' });
            int n;
            int k;
            Int32.TryParse(numbers[0], out n);
            Int32.TryParse(numbers[1], out k);

            Dictionary<int, string> words = new Dictionary<int, string>();
            List<string> solutions = new List<string>();
            List<string> rejected = new List<string>();


            for (int i = 1; i < wordArray.Count; i++)
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
