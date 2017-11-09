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
            SortedDictionary<int, List<int> > Dict = new SortedDictionary<int, List<int>>();
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
                    Dict[time].Add(money);
                    Dict[time].Sort();
                }
                else
                {
                    List<int> listOfPeople = new List<int>();
                    listOfPeople.Add(money);
                    Dict.Add(time, listOfPeople);
                }
            }

            for (int i = T; i >= 0; i--)
            {
                if(Dict.ContainsKey(i))
                {
                    int temp = Dict[i].Last();
                    int index = i;

                    for (int j = i+1; j < T; j++)
                    {
                        if (Dict.ContainsKey(j) && Dict[j].Count > 0)
                        {
                            if(Dict[j].Last() > temp)
                            {
                                temp = Dict[j].Last();
                                index = j;
                            }
                        }
                    }

                    Dict[index].Remove(temp);
                    Dict[index].Sort();

                    sum += temp;
                }
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
