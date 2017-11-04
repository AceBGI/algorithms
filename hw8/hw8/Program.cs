using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw8
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<int, SortedDictionary<int,int> > Dict = new SortedDictionary<int, SortedDictionary<int,int>>();
            int sum = 0;

            string firstLine = Console.ReadLine();
            string[] fLine = firstLine.Split(new[] { '\t', ' ' });
            int N;
            int T;
            Int32.TryParse(fLine[0], out N);
            Int32.TryParse(fLine[1], out T);

            for (int i = 0; i < N; i++)
            {
                string lineInput = Console.ReadLine();
                string[] line = lineInput.Split(new[] { '\t', ' ' });
                int money;
                int time;
                Int32.TryParse(line[0], out money);
                Int32.TryParse(line[1], out time);

                if (Dict.ContainsKey(time))
                {
                    Dict[time].Add(money,money);
                }
                else
                {
                    SortedDictionary<int, int> newPerson = new SortedDictionary<int, int>();
                    newPerson.Add(money, money);
                    Dict.Add(time, newPerson);
                }
            }

            for (int i = 0; i <= T; i++)
            {
                int temp = 0;
                if (Dict.ContainsKey(i))
                {
                    temp = Dict[i].Last().Value;
                    Dict.Remove(i);
                }

                foreach (SortedDictionary<int, int> item in Dict.Values)
                {
                    if(item.Count > 1)
                    {
                        if(item.ElementAt((item.Count - 1) - 1).Value > temp)
                        {
                            temp = item.ElementAt((item.Count - 1) - 1).Value;
                            Dict.Remove(item.ElementAt((item.Count - 1) - 1).Key);
                            break;
                        }
                    }
                }

                sum += temp;
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
